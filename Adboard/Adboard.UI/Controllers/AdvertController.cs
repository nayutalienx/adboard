using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adboard.Contracts;
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Category;
using Adboard.Contracts.DTOs.Comment;
using Adboard.Contracts.DTOs.Paging;
using Adboard.Contracts.DTOs.Photo;
using Adboard.UI.Clients;
using Adboard.UI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adboard.UI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AdvertController : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IIdentityClient _identityClient;
        private readonly IMapper _mapper;

        public AdvertController(
            IAdvertApiClient advertApiClient,
            ICategoryApiClient categoryApiClient,
            IIdentityClient identityClient,
            IMapper mapper) {
            _advertApiClient = advertApiClient;
            _categoryApiClient = categoryApiClient;
            _identityClient = identityClient;
            _mapper = mapper;
        }

        [Authorize]
        [Route("Edit/{id:long}")]
        public async Task<IActionResult> EditAdvert(long id) {

            var response = await _advertApiClient.GetAdvertsByFilterAsync(new AdvertFilter { AdvertId = id, Size = 1, CurrentPage = 1 });
            var ad = response.Data.FirstOrDefault();
            ViewBag.Advert = ad;
            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;

            if (response.HasErrors)
                return View("Error", new ErrorViewModel { RequestId = response.Errors.FirstOrDefault() });
            return View();
        }

        [Authorize]
        [Route("Edit/{id:long}")]
        [HttpPost]
        public async Task<IActionResult> EditAdvert(UpdateAdvertViewModel advert) {
            if (string.IsNullOrEmpty(advert.Header) ||
                advert.CategoryId == 0)
                return View("Error", new ErrorViewModel { RequestId = "Enter all data" });
            var dto = _mapper.Map<UpdateAdvertDto>(advert);
            if (advert.Photo?.Count > 0)
            {
                List<PhotoDto> photoList = new List<PhotoDto>();
                foreach (var photo in advert.Photo)
                {
                    byte[] p1 = null;
                    using (var fs1 = photo.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }

                    photoList.Add(new PhotoDto { Data = p1 });
                }
                dto.Photo = photoList.ToArray();
            }

            var cats = await _categoryApiClient.GetCategoriesAsync();
            IReadOnlyCollection<CategoryDto> _cats = cats.Data;
            ViewBag.Categories = _cats;
            ApiResponse<AdvertDto> response = null;
            try
            {
                response = await _advertApiClient.UpdateAdvertAsync(dto);
            }
            catch (ApplicationException ex) {
                if (ex.Message.Equals("401"))
                    return Redirect($"~/Home/Logout");
                else
                    return View("Error", new ErrorViewModel{ RequestId = ex.Message });
            }

            if (response.HasErrors)
                return View("Error", new ErrorViewModel { RequestId = response.Errors.FirstOrDefault() });
            AdvertDto result = response.Data;
            return Redirect($"~/Advert/{result.Id}");
        }

        [Authorize]
        [Route("Delete/{id:long}")]
        [HttpGet]
        public async Task<IActionResult> DeleteAdvert(long id) {
            

            ApiResponse response = null;
            try
            {
                response = await _advertApiClient.RemoveAdvertAsync(new RemoveAdvertDto
                {
                    AdvertId = id,
                    UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value
                });
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Equals("401"))
                    return Redirect($"~/Home/Logout");
                else
                    return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }


            return Redirect("~/");
        }

        
        [Route("{id:long}")]
        public async Task<IActionResult> AdvertById(long id)
        {
            var response = await _advertApiClient.GetAdvertsByFilterAsync(new AdvertFilter { AdvertId = id, Size = 1, CurrentPage = 1 });
            
            var ad = response.Data.FirstOrDefault();
            ViewBag.Advert = ad;
            var author = await _identityClient.GetUserInfoAsync(ad.UserId);
            var author_dto = author.Data;
            if (author_dto.Username.Length > 30)
                author_dto.Username = "";
            ViewBag.UserInfo = author_dto;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;

            if (ad.Comments.Length > 0) {
                IDictionary<string, string> id_name = new Dictionary<string, string>();
                foreach (var comment in ad.Comments) {
                    if (id_name.ContainsKey(comment.UserId))
                        continue;

                    var commentAuthor = await _identityClient.GetUserInfoAsync(comment.UserId);
                    var commentAuthorData = commentAuthor.Data;
                    if(commentAuthorData != null)
                        id_name.Add(comment.UserId, commentAuthorData.Email);
                    else
                        id_name.Add(comment.UserId, "User deleted");
                }
                ViewBag.CommentAuthors = id_name;
            }

            if (User.Identity.IsAuthenticated)
            {
                string usid = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
                string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
                ViewBag.EditAccess = (usid.Equals(ad.UserId) || role.Equals("Admin"));
            }
            else
                ViewBag.EditAccess = false;

            if (response.HasErrors)
                return View("Error", new ErrorViewModel { RequestId = response.Errors.FirstOrDefault() });
            return View();
        }

        [Authorize]
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> AddAdvert() {
            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            return View();
        }


        

        


        [Authorize]
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddAdvert(NewAdvertViewModel advert)
        {
            advert.UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            if (string.IsNullOrEmpty(advert.Header) ||
                string.IsNullOrEmpty(advert.Description) ||
                advert.CategoryId == 0)
                return View("Error", new ErrorViewModel { RequestId = "Enter all data" });

            var dto = _mapper.Map<NewAdvertDto>(advert);
            if (advert.Photo?.Count > 0)
            {
                List<PhotoDto> photoList = new List<PhotoDto>();
                foreach (var photo in advert.Photo)
                {
                    byte[] p1 = null;
                    using (var fs1 = photo.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }

                        photoList.Add(new PhotoDto { Data = p1 });
                }
                dto.Photo = photoList.ToArray();
            }


            
            ApiResponse<AdvertDto> response = null;
            try
            {
                response = await _advertApiClient.AddAdvertAsync(dto);   
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Equals("401"))
                    return Redirect($"~/Home/Logout");
                else
                    return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }




            if (response.HasErrors)
                return Ok(response.Errors.FirstOrDefault());
            AdvertDto result = response.Data;
            return Redirect($"Advert/{result.Id}");
        }

        [Route("Filter")]
        [HttpGet]
        public async Task<IActionResult> Filter(
            [FromQuery] string Header = default,
            [FromQuery] string Description = default,
            [FromQuery] int CategoryId = default,
            [FromQuery] string HasPhotoOnly = "false",
            [FromQuery] uint? PriceFrom = null,
            [FromQuery] uint? PriceTo = null,
            [FromQuery] string UserId = default,
            [FromQuery] long? AdvertId = null,
            [FromQuery] DateTime? CreatedDateTimeFrom = null,
            [FromQuery] DateTime? CreatedDateTimeTo = null,
            [FromQuery]string Region = default,
            [FromQuery] int Size = 6,
            [FromQuery]string in_header = "true",
            [FromQuery]int page = 1
            ) 
        {
            ViewData["Header"] = Header;
            ViewData["Description"] = Description;
            ViewData["CategoryId"] = CategoryId;
            ViewData["HasPhotoOnly"] = HasPhotoOnly;
            ViewData["PriceFrom"] = PriceFrom;
            ViewData["PriceTo"] = PriceTo;
            ViewData["UserId"] = UserId;
            ViewData["AdvertId"] = AdvertId;
            ViewData["CreatedDateTimeFrom"] = CreatedDateTimeFrom;
            ViewData["CreatedDateTimeTo"] = CreatedDateTimeTo;
            ViewData["Region"] = Region;
            ViewData["Size"] = Size;
            ViewData["in_header"] = in_header;
            ViewData["page"] = page;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            IReadOnlyCollection<CategoryDto> cat_data = cats.Data;
            ViewBag.Categories = cat_data;
            var _filter = new FilterAdvertViewModel
            {
                Header = Header,
                Description = Description,
                CategoryId = (CategoryId == 0) ? (long?)null : CategoryId,
                HasPhotoOnly = bool.Parse(HasPhotoOnly),
                PriceFrom = PriceFrom,
                PriceTo = PriceTo,
                UserId = UserId,
                AdvertId = AdvertId,
                CreatedDateTimeFrom = CreatedDateTimeFrom,
                CreatedDateTimeTo = CreatedDateTimeTo,
                Region = Region,
                Size = Size
                
            };
            if (in_header.Equals("false"))
                _filter.Description = Header;

            var viewFilter = _filter;
            int p = page;
            AdvertFilter filter = _mapper.Map<AdvertFilter>(viewFilter);
            filter.CurrentPage = p;

            if (viewFilter.CreatedDateTimeTo.HasValue || viewFilter.CreatedDateTimeFrom.HasValue)
            {
                var range = new Range<DateTime>();

                if (viewFilter.CreatedDateTimeFrom.HasValue)
                    range.From = viewFilter.CreatedDateTimeFrom.Value;
                else
                    range.From = DateTime.MinValue;

                if (viewFilter.CreatedDateTimeTo.HasValue)
                    range.To = viewFilter.CreatedDateTimeTo.Value;
                else
                    range.To = DateTime.MaxValue;
                filter.CreatedDateTime = range;
            }

            if (viewFilter.PriceTo.HasValue || viewFilter.PriceFrom.HasValue)
            {
                var range = new Range<uint>();

                if (viewFilter.PriceFrom.HasValue)
                    range.From = viewFilter.PriceFrom.Value;
                else
                    range.From = UInt32.MinValue;

                if (viewFilter.PriceTo.HasValue)
                    range.To = viewFilter.PriceTo.Value;
                else
                    range.To = UInt32.MaxValue;
                filter.Price = range;
            }

            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            
            IReadOnlyCollection<AdvertDto> _ads = response.Data;
            ViewBag.Adverts = _ads;
            ViewBag.CurrentPage = p;
            ViewBag.Size = filter.Size;

            if (response.HasErrors)
                return View("Error", new ErrorViewModel { RequestId = response.Errors.FirstOrDefault() });
            return View(_filter);
        }


        [Route("{id:long}")]
        [HttpPost]
        public async Task<IActionResult> AdvertById(NewCommentDto comment) {
            if (comment.Text == null)
                return View("Error", new ErrorViewModel { RequestId = "Enter text" });
            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            comment.UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            


            ApiResponse<CommentDto> response = null;
            try
            {
                response = await _advertApiClient.AddCommentAsync(comment);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message.Equals("401"))
                    return Redirect($"~/Home/Logout");
                else
                    return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }



            return Redirect($"{comment.AdvertId}");
        }

        [Route("PhotoModal")]
        [HttpGet]
        public async Task<IActionResult> PhotoModal([FromQuery] int advert_id, [FromQuery] int photo_id) {
            var response = await _advertApiClient.GetAdvertsByFilterAsync(new AdvertFilter { AdvertId = advert_id, Size = 1, CurrentPage = 1 });
            AdvertDto ad = response.Data.FirstOrDefault();
            ViewBag.PhotoData = ad.Photo.Where(p => p.Id == photo_id).FirstOrDefault().Data;
            return PartialView();
        }




        

    }
}
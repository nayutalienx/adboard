using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Comment;
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
                    photoList.Add( new PhotoDto { Data = p1 } );
                }
                dto.Photo = photoList.ToArray();
            }
            var response = await _advertApiClient.UpdateAdvertAsync(dto);
            AdvertDto result = response.Data;
            return Redirect($"~/Advert/{result.Id}");
        }

        [Authorize]
        [Route("Delete/{id:long}")]
        [HttpGet]
        public async Task<IActionResult> DeleteAdvert(long id) {
            await _advertApiClient.RemoveAdvertAsync(new RemoveAdvertDto 
            { 
                AdvertId = id,
                UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value
        });
            return Redirect("~/");
        }

        [Route("{id:long}")]
        public async Task<IActionResult> AdvertById(long id)
        {
            var response = await _advertApiClient.GetAdvertsByFilterAsync(new AdvertFilter { AdvertId = id, Size = 1, CurrentPage = 1 });
            var ad = response.Data.FirstOrDefault();
            ViewBag.Advert = ad;
            var author = await _identityClient.GetUserInfoAsync(ad.UserId);
            ViewBag.UserInfo = author.Data;

            if (ad.Comments.Length > 0) {
                IDictionary<string, string> id_name = new Dictionary<string, string>();
                foreach (var comment in ad.Comments) {
                    if (id_name.ContainsKey(comment.UserId))
                        continue;

                    var commentAuthor = await _identityClient.GetUserInfoAsync(comment.UserId);
                    var commentAuthorData = commentAuthor.Data;
                    id_name.Add(comment.UserId, commentAuthorData.Username);
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
                    return View("Error",new ErrorViewModel { RequestId = "Enter all data"});

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
            var response = await _advertApiClient.AddAdvertAsync(dto);
            AdvertDto result = response.Data;
            return Redirect($"Advert/{result.Id}");
        }

        [Route("{id:long}")]
        [HttpPost]
        public async Task<IActionResult> AdvertById(NewCommentDto comment) {
            if (comment.Text == null)
                return View("Error", new ErrorViewModel { RequestId = "Enter text" });

            comment.UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            await _advertApiClient.AddCommentAsync(comment);
            return Redirect($"{comment.AdvertId}");
        }

        

    }
}
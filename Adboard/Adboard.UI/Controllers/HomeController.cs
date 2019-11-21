using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adboard.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Adboard.UI.Clients;
using Adboard.Contracts.DTOs.Advert;
using AutoMapper;
using Adboard.Contracts.DTOs.Paging;

namespace Adboard.UI.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IMapper _mapper;
        private readonly IIdentityClient _identityClient;

        public HomeController(IAdvertApiClient advertApiClient, ICategoryApiClient categoryApiClient,
            IIdentityClient identityClient,
            IMapper mapper)
        {
            _mapper = mapper;
            _advertApiClient = advertApiClient;
            _categoryApiClient = categoryApiClient;
            _identityClient = identityClient;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            
            AdvertFilter filter = new AdvertFilter { CurrentPage = 1, Size = 6 };
            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            ViewBag.Size = 30;
            ViewBag.CurrentPage = 1;
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Index(FilterAdvertViewModel viewFilter, int? page)
        {
            AdvertFilter filter = _mapper.Map<AdvertFilter>(viewFilter);
            filter.CurrentPage = 1;
            

            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            ViewBag.CurrentPage = 1;
            ViewBag.Size = filter.Size;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var cats = await _categoryApiClient.GetCategoriesAsync();
            string userId = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value;
            ViewBag.Role = User.Claims.FirstOrDefault(x => x.Type.Contains("role")).Value;


            var user = await _identityClient.GetUserInfoAsync(userId);
            var user_dto = user.Data;

            LoginViewModel model = new LoginViewModel {
                NameEmail = user_dto.Email,
                Phone = user_dto.PhoneNumber ?? ""
            };

           


            AdvertFilter filter = new AdvertFilter { CurrentPage = 1, Size = 20, UserId = userId };
            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;
            
            ViewBag.Categories = cats.Data;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            var response = await _identityClient.UpdateUserInfoAsync(new UserDto 
            { 
                Id = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value,
                Email = model.NameEmail ?? "",
                Username = model.NameEmail ?? "",
                PhoneNumber = model.Phone ?? ""
        });
            if (!response.HasErrors)
            {
                ViewBag.Role = User.Claims.FirstOrDefault(x => x.Type.Contains("role")).Value;
                AdvertFilter filter = new AdvertFilter 
                { 
                    CurrentPage = 1, Size = 20, UserId = User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value
                };
                var advert_response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
                ViewBag.Adverts = advert_response.Data;
                ViewBag.EditSuccess = true;
                var cats = await _categoryApiClient.GetCategoriesAsync();
                ViewBag.Categories = cats.Data;
                model.NameEmail = (model.NameEmail == null) ? "" : model.NameEmail;
                model.Phone = (model.Phone == null) ? "" : model.Phone;
                return View(model);
            } else 
            {
                return View("Error", new ErrorViewModel { RequestId = response.Errors.FirstOrDefault() });
            }
        }

       
        public async Task<IActionResult> Logout()
        {
            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            return SignOut("Cookies", "oidc");
        }
    }
}

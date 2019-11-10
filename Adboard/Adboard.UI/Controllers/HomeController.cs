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

        public HomeController(IAdvertApiClient advertApiClient, ICategoryApiClient categoryApiClient,
            IMapper mapper)
        {
            _mapper = mapper;
            _advertApiClient = advertApiClient;
            _categoryApiClient = categoryApiClient;
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
            ViewBag.Size = 6;
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
            ViewBag.Categories = cats.Data;
            return View();
        }

       
        public async Task<IActionResult> Logout()
        {
            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            return SignOut("Cookies", "oidc");
        }
    }
}

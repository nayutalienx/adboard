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

namespace Adboard.UI.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public HomeController(IAdvertApiClient advertApiClient, ICategoryApiClient categoryApiClient)
        {
            _advertApiClient = advertApiClient;
            _categoryApiClient = categoryApiClient;
        }
        
        
        public async Task<IActionResult> Index()
        {
            AdvertFilter filter = new AdvertFilter { CurrentPage = 0, Size = 10 };
            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            return View();
        }

        [Authorize]
        public IActionResult Login()
        {
            return View();
        }

       
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}

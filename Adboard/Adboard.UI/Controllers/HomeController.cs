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

        public HomeController(IAdvertApiClient advertApiClient) =>
            _advertApiClient = advertApiClient;
        
        
        public async Task<IActionResult> Index()
        {
            AdvertFilter filter = new AdvertFilter { CurrentPage = 0, Size = 10 };
            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;

            return View(new UserViewModel 
            { 
                Name = User.Claims?.Where(x => x.Type.Equals("name")).FirstOrDefault()?.Value ?? "",
                Role = User.Claims?.Where(x => x.Type.Contains("role")).FirstOrDefault()?.Value ?? "",              
                IsAuthenticated = User.Identity.IsAuthenticated
            });
        }

        [Authorize]
        public IActionResult Login()
        {
            return View(new UserViewModel
            {
                Name = User.Claims?.Where(x => x.Type.Equals("name")).FirstOrDefault()?.Value ?? "",
                Role = User.Claims?.Where(x => x.Type.Contains("role")).FirstOrDefault()?.Value ?? "",                
                IsAuthenticated = User.Identity.IsAuthenticated
            });

        }

       
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}

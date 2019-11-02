using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adboard.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Adboard.UI.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            
            return View(new UserViewModel 
            { 
                Name = User.Claims?.Where(x => x.Type.Equals("name")).FirstOrDefault()?.Value ?? "",
                Role = User.Claims?.Where(x => x.Type.Equals("role")).FirstOrDefault()?.Value ?? "",              
                IsAuthenticated = User.Identity.IsAuthenticated
            });
        }

        [Authorize]
        public IActionResult Login()
        {
            return View(new UserViewModel
            {
                Name = User.Claims?.Where(x => x.Type.Equals("name")).FirstOrDefault()?.Value ?? "",
                Role = User.Claims?.Where(x => x.Type.Equals("role")).FirstOrDefault()?.Value ?? "",                
                IsAuthenticated = User.Identity.IsAuthenticated
            });

        }

       
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}

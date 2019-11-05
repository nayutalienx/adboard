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
        [Route("{page?}")]
        public async Task<IActionResult> Index(int? page)
        {
            int p;
            if (page.HasValue && page.Value > 0)
                p = page.Value;
            else
                p = 1;
            AdvertFilter filter = new AdvertFilter { CurrentPage = p, Size = 3 };
            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            ViewBag.Size = 3;
            ViewBag.CurrentPage = p;
            return View();
        }

        [Route("{page?}")]
        [HttpPost]
        public async Task<IActionResult> Index(FilterAdvertViewModel viewFilter, int? page)
        {
            int p;
            if (page.HasValue && page.Value > 0)
                p = page.Value;
            else
                p = 1;
            AdvertFilter filter = _mapper.Map<AdvertFilter>(viewFilter);
            filter.CurrentPage = p;
            if (viewFilter.CreatedDateTimeTo.HasValue) 
            {
                var range = new Range<DateTime> 
                { 
                    To = viewFilter.CreatedDateTimeTo.Value
                };

                if (viewFilter.CreatedDateTimeFrom.HasValue)
                    range.From = viewFilter.CreatedDateTimeFrom.Value;
                else
                    range.From = DateTime.Now;
                filter.CreatedDateTime = range;
            }

            if (viewFilter.PriceTo.HasValue)
            {
                var range = new Range<uint>
                {
                    To = viewFilter.PriceTo.Value
                };

                if (viewFilter.PriceFrom.HasValue)
                    range.From = viewFilter.PriceFrom.Value;
                else
                    range.From = UInt32.MaxValue;
                filter.Price = range;
            }

            var response = await _advertApiClient.GetAdvertsByFilterAsync(filter);
            ViewBag.Adverts = response.Data;

            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            ViewBag.CurrentPage = p;
            ViewBag.Size = filter.Size;
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

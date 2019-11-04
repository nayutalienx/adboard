using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adboard.Contracts.DTOs.Advert;
using Adboard.UI.Clients;
using Adboard.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Adboard.UI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AdvertController : Controller
    {
        private readonly IAdvertApiClient _advertApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public AdvertController(IAdvertApiClient advertApiClient, ICategoryApiClient categoryApiClient) {
            _advertApiClient = advertApiClient;
            _categoryApiClient = categoryApiClient;
        }

        [Route("{id:long}")]
        public async Task<IActionResult> AdvertById(long id)
        {
            var response = await _advertApiClient.GetAdvertsByFilterAsync(new AdvertFilter { AdvertId = id, Size = 1 });
            ViewBag.Advert = response.Data.FirstOrDefault();
            return View();
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> AddAdvert() {
            var cats = await _categoryApiClient.GetCategoriesAsync();
            ViewBag.Categories = cats.Data;
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddAdvert(NewAdvertViewModel advert)
        {
            return Ok(advert);
        }
    }
}
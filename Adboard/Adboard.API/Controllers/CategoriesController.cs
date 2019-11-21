using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Adboard.Contracts.DTOs.Category;
using BusinessLogicLayer.Abstraction;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Adboard.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            return ApiResult(await _categoryManager.GetAllCategoriesAsync());
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] NewCategoryDto category)
        {
            return ApiResult(await _categoryManager.AddCategoryAsync(category));
        }
    }
}
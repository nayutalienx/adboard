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
    public class CategoriesController : ControllerBase
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
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), statusCode: (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllCategoriesAsync()
        {
            var result = await _categoryManager.GetAllCategoriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> AddCategoryAsync([FromBody] NewCategoryDto category)
        {
            await _categoryManager.AddCategoryAsync(category);
            return NoContent();
        }
    }
}
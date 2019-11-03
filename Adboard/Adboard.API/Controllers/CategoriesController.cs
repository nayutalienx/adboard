using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult GetAllCategories()
        {
            var result = _categoryManager.GetAllCategories();
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
        public ActionResult AddCategory([FromBody] NewCategoryDto category)
        {
            _categoryManager.AddCategory(category);
            return NoContent();
        }
    }
}
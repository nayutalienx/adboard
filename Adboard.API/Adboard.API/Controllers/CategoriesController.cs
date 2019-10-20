using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Category;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Adboard.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController()
        {
            var serviceCollection = new ServiceCollection()
                .Install()
                .BuildServiceProvider();

            _categoryManager = serviceCollection.GetService<ICategoryManager>();
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
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
        [HttpPost("categories")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        public ActionResult AddCategory([FromBody] NewCategoryDto category)
        {
            _categoryManager.AddCategory(category);
            return NoContent();
        }
    }
}
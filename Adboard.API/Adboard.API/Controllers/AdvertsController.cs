using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Advert;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Adboard.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertManager _advertManager;

        public AdvertsController() {
            var serviceCollection = new ServiceCollection()
                .Install()
                .BuildServiceProvider();

            _advertManager = serviceCollection.GetService<IAdvertManager>();
        }
        /// <summary>
        /// Get all adverts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdvertDto>), statusCode: (int)HttpStatusCode.OK)]
        public ActionResult GetAll() {
            var result = _advertManager.GetAll();
            return Ok(result);
        }
    }
}
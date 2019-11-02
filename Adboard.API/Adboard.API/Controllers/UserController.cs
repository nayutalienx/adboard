using AdvertPlatform.WebApi.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dashboard.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {

        protected ApiResult ApiResult(object @object)
             => new ApiResult(@object);

        /// <summary>
        /// User info
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            return Ok("User") ;
        }

        /// <summary>
        /// Admin info
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [Route("admin")]
        [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult AdminInfo()
        {
            return Ok("Admin");
        }

        
    }
}
using Adboard.API.Controllers;
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
    public class UserController : ApiController
    {
        
        /// <summary>
        /// User info
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {

            return ApiResult("User");
        }

        /// <summary>
        /// Admin info
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [Route("admin")]
        [HttpGet]
        public async Task<IActionResult> AdminInfo()
        {
            return ApiResult("Admin");
        }

        
    }
}
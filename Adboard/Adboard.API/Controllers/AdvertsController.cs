
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Comment;
using BusinessLogicLayer.Abstraction;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Adboard.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AdvertsController : ApiController
    {
        private readonly IAdvertManager _advertManager;

        public AdvertsController(IAdvertManager advertManager) {
            _advertManager = advertManager;
        }
        /// <summary>
        /// Get all adverts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAdvertsAsync() {
            return ApiResult(await _advertManager.GetAllAsync());
        }
        

        /// <summary>
        /// Add advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAdvertAsync([FromBody] NewAdvertDto advert)
        {
            string id = User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
            if (!advert.UserId.Equals(id))
                throw new Exception($"{nameof(advert)} Access denied");
            return ApiResult(await _advertManager.CreateAsync(advert));
        }



        

        /// <summary>
        /// Update advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAdvertAsync([FromBody] UpdateAdvertDto advert)
        {
            string id = User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
            if (!advert.UserId.Equals(id))
                throw new Exception($"{nameof(advert)} Access denied");
            return ApiResult(await _advertManager.UpdateAsync(advert));
        }

        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost("comments")]
        [Authorize]
        public async Task<IActionResult> AddCommentAsync(NewCommentDto comment)
        {
            return ApiResult(await _advertManager.AddCommentAsync(comment));
        }

        /// <summary>
        /// Get advert by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ad = await _advertManager.GetAdvertsByFilterAsync(new AdvertFilter { AdvertId = id }); ;
            if (ad.Items == null)
                return NotFound();

            return ApiResult(ad.Items);
        }

        /// <summary>
        /// Get adverts by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByFilterAsync([FromBody] AdvertFilter filter) {
            return ApiResult(await _advertManager.GetAdvertsByFilterAsync(filter));
        }

        /// <summary>
        /// Delete advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAdvertAsync([FromBody] RemoveAdvertDto advert)
        {
            string id = User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
            if (!advert.UserId.Equals(id))
                throw new Exception($"{nameof(advert)} Access denied");
            await _advertManager.RemoveAsync(advert);
            return ApiResult("Deleted.");
        }
    }
}
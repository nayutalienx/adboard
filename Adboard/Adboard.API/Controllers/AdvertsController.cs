
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class AdvertsController : ControllerBase
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
        [ProducesResponseType(typeof(IEnumerable<AdvertDto>), statusCode: (int)HttpStatusCode.OK)]
        public ActionResult GetAllAdverts() {
            var result = _advertManager.GetAll();
            return Ok(result);
        }
        

        /// <summary>
        /// Add advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK)]
        public ActionResult AddAdvert([FromBody] NewAdvertDto advert)
        {
            string id = User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
            if (!advert.UserId.Equals(id))
                throw new Exception($"{nameof(advert)} Access denied");
            _advertManager.Create(advert);
            return Ok("Advert added.");
        }



        

        /// <summary>
        /// Update advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK)]
        public ActionResult UpdateAdvert([FromBody] UpdateAdvertDto advert)
        {
            string id = User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
            if (!advert.UserId.Equals(id))
                throw new Exception($"{nameof(advert)} Access denied");
            _advertManager.Update(advert);
            return Ok("Updated.");
        }

        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost("comments")]
        [Authorize]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK)]
        public ActionResult AddComment(NewCommentDto comment)
        {
            _advertManager.AddComment(comment);
            return Ok("Comment added.");
        }

        /// <summary>
        /// Get advert by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AdvertDto), statusCode: (int) HttpStatusCode.OK)]
        public ActionResult Get(int id)
        {
            AdvertDto ad = _advertManager.GetAdvertsByFilter(new AdvertFilter { AdvertId = id }).Items.ToArray()[0]; ;
            if (ad == null)
                return NotFound();

            return Ok(ad);
        }

        /// <summary>
        /// Get adverts by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("filter")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AdvertDto>), statusCode: (int)HttpStatusCode.OK)]
        public ActionResult GetByFilter([FromBody] AdvertFilter filter) {
            var result = _advertManager.GetAdvertsByFilter(filter);
            return Ok(result.Items);
        }

        /// <summary>
        /// Delete advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK)]
        public ActionResult DeleteAdvert([FromBody] RemoveAdvertDto advert)
        {
            string id = User.Claims.Where(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
            if (!advert.UserId.Equals(id))
                throw new Exception($"{nameof(advert)} Access denied");
            _advertManager.Remove(advert);
            return Ok("Deleted.");
        }
    }
}
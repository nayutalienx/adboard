
using System.Collections.Generic;

using System.Net;

using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Advert;

using BusinessLogicLayer.Objects.Comment;

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
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        public ActionResult AddAdvert([FromBody] NewAdvertDto advert)
        {
            _advertManager.Create(advert);
            return NoContent();
        }



        

        /// <summary>
        /// Update advert
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        public ActionResult UpdateAdvert([FromBody] UpdateAdvertDto advert)
        {
            _advertManager.Update(advert);
            return NoContent();
        }

        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost("comments")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        public ActionResult AddComment(NewCommentDto comment)
        {
            _advertManager.AddComment(comment);
            return NoContent();
        }

        /// <summary>
        /// Get advert by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
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
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        public ActionResult DeleteAdvert([FromBody] RemoveAdvertDto advert)
        {
            _advertManager.Remove(advert);
            return NoContent();
        }
    }
}
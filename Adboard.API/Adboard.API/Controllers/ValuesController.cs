using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Adboard.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/values/5
        [HttpGet("{value}")]
        public ActionResult<string> GetValue(int value)
        {
            return Ok($"value is {value}!");
        }

        // GET api/v1/values/5/filter
        [HttpGet("{value}/filter")]
        [ProducesResponseType(typeof(int), statusCode: (int)HttpStatusCode.OK)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        public ActionResult<string> GetValueWithFilter([FromRoute] int value)
        {
            return Ok($"{value}/filter was requested");
        }

        // GET api/v1/values/5/template
        [HttpGet("{value:int}/template")]
        public ActionResult<string> GetValueWithTemplate(int value)
        {
            return Ok($"{value}/template");
        }

        // GET api/v1/values/5/route
        [HttpGet("{value}/route")]
        public ActionResult<string> GetValueFromRoute([FromRoute] int value)
        {
            return Ok($"the value in your route is {value}");
        }

        // GET api/v1/values/body
        [HttpGet("body")]
        public ActionResult<string> GetValueFromBody([FromBody] int value) {
            return Ok($"the value in your body is {value}");
        }

        // GET api/v1/values/query?value=5
        [HttpGet("query")]
        public ActionResult<string> GetValueFromQuery([FromQuery] int value)
        {
            return Ok($"the value in your query is {value}");
        }

        /// <summary>
        /// GET api/v1/values/header
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet("header")]
        public ActionResult<string> GetValueFromHeader([FromHeader] int value)
        {
            return Ok($"the value in header is {value}");
        }

        // POST api/v1/values
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            return Ok($"ill try to create {value}");
        }

        // PUT api/v1/values/5
        [HttpPut]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return Ok($"put {id}, {value}");
        }

        // DELETE api/v1/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok($"delete {id}");
        }
    }
}

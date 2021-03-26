using System.Collections.Generic;
using System.Web.Http;

namespace ExampleOauth2FullFramework.Controllers
{
    public class HelloWorldController : ApiController
    {
        // GET: api/HelloWorld
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello World" };
        }

        [Authorize]
        // POST: api/HelloWorld/5
        public IHttpActionResult Post([FromBody] string value)
        {
            return Ok("Hello World");
        }
    }
}

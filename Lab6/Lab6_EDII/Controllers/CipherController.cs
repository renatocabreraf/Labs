using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab6_EDII.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        // GET: api/Cipher
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Cipher/5
        [Route("getPublicKey")] //API  "/cipher/getPublicKey"
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cipher
        [Route ("ceasar2")] //API "/cipher/caesar2/"        
        [HttpPost("{name}/{method}")]        
        public void Post([FromForm] string value)
        {
            //generar PublicKey

        }

        // PUT: api/Cipher/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
              

    }
}

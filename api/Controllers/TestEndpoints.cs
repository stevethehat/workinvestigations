using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Dapper;

namespace Async.Controllers
{
    [Route("endpoints")]
    public class TestEndpoints : Controller
    {
        // GET api/values
        private async Task<List<string>> GetStrings()
        {
            await Task.Delay(1000);
            List<string> result = new List<string>();
            result.Add("value 1");
            result.Add("value 2");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
            var result = await GetStrings();
            return Ok(result);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

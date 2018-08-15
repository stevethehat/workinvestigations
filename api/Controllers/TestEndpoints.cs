using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Dapper;
using Test.Interfaces;

namespace Async.Controllers
{
    [Route("endpoints")]

    /// <summary>
    /// this is a summary in the comment for the class.
    /// </summary>
    public class TestEndpoints : Controller
    {
        private ILogger<TestEndpoints> _logger {get;set;}
        public TestEndpoints(ILogger<TestEndpoints> logger){
            _logger = logger;
        }
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
            _logger.LogInformation(1, "test", 1);
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

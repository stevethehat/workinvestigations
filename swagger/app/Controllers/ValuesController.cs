using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Swagger;

namespace SwaggerTests
{
    /// <summary>
    /// This is the test response item
    /// </summary>
    /// <remarks>
    /// Extra remarks for more info on this object.
    /// It is multi line
    /// = Markup = 
    /// ??
    /// </remarks>
    public class TestResponse{
        /// <summary>
        /// Value 1
        /// </summary>
        /// <value>a test string</value>
        [DataType("GOLDCUSTOMER")]
        public string Value1 { get; set; }
        /// <summary>
        /// Value 2
        /// </summary>
        /// <value>this is an int</value>
        
        [Required]
        [StringLength(100)]
        public int Value2 { get; set; }
        /// <summary>
        /// Decimal test
        /// </summary>
        /// <value>not sure what this will do</value>
        public Decimal DecimalTest { get; set; }
        public List<TestResponseItem> Items { get; set; }
    }

    public class TestResponseItem{
        public string Name { get; set; }
    }


    /// <summary>
    /// A Controller for testing swagger stuff
    /// </summary>
    /// <remarks>where willl this show??</remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<TestResponse> Get()
        {
            TestResponse result = new TestResponse(){
                Value1 = "this is value 1",
                Value2 = 99,
                Items = new List<TestResponseItem>()
            };

            result.Items.Add(
                new TestResponseItem(){
                    Name = "item 1"
                }
            );
            return result;
        }

        /// <summary>
        /// Get a TestResponse
        /// </summary>
        /// <param name="id">The id of the item to return</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<TestResponseItem> Get(int id)
        {
            return new TestResponseItem(){ Name = "a new test response" };
        }

        /// <summary>
        /// Update an item 
        ///
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Post([FromBody] string value)
        {
        }

        /*
        // GET api/values/5

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}

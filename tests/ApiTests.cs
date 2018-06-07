using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Tests
{
    [Collection("Database Collection")]
    public class ApiTests
    {
        public TestServer testServer {get;set;} 
        public DatabaseFixture databaseFixture {get;set;}
        public ApiTests(DatabaseFixture databaseFixture){
            var host = new WebHostBuilder().
                UseEnvironment("Development").
                UseStartup<Api.Startup>().
                UseApplicationInsights();

            testServer = new TestServer(host);
        }
        [Fact]
        public async void Test1()
        {
            var client = testServer.CreateClient();
            HttpResponseMessage response = await client.GetAsync("endpoints");

            string responseText = await response.Content.ReadAsStringAsync();
            string expected = "[\"value 1\",\"value 2\"]";
            Assert.Equal(responseText, expected);
        }
    }
}

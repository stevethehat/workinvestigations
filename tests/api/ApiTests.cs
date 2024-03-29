using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

using Api.Util;

namespace Tests
{
    [Collection("Database Collection")]
    public class ApiTests
    {
        public TestServer testServer {get;set;} 
        public DatabaseFixture databaseFixture {get;set;}
        public ApiTests(DatabaseFixture df){
            var host = new WebHostBuilder().
                UseEnvironment("Development").
                UseStartup<Api.Startup>().
                UseApplicationInsights();

            testServer = new TestServer(host);
            databaseFixture = df;
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

        private int CallLambda(Func<int, int, int> a){
            if(a == null){
                return -1;
            } else {
                return a(1, 2);
            }
        }

        [Fact]
        public void LambdaTest(){
            int result;
            result = CallLambda((x,y) => x*y);
            Assert.Equal(result, 2);
        }
        [Fact]
        public void LambdaTest2(){
            int result;
            result = CallLambda(null);
            Assert.Equal(result, -1);
        }
        [Fact]
        public void LambdaTest3(){
            int result;
            int whatWeWant = 22;
            result = CallLambda((x,y) => whatWeWant);
            Assert.Equal(result, 22);
        }
    }
}

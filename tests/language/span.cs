using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

using Api.Util;

namespace Tests
{
    [Collection("Database Collection")]
    public class SpanTests
    {
        public DatabaseFixture databaseFixture {get;set;}
        public SpanTests(DatabaseFixture df){
            databaseFixture = df;
        }

        private int CallLambda(Func<int, int, int> a){
            if(a == null){
                return -1;
            } else {
                return a(1, 2);
            }
        }

        [Fact]
        public void CreateSpan(){
            int result;
            string test = "hello this is a test string";
            Span<char> testSpan = new Span<char>(test.ToCharArray());
            result = CallLambda((x,y) => x*y);
            Assert.Equal(result, 2);
        }
    }
}

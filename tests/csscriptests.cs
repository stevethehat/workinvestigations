using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Dapper;

using Xunit;

using Api.Util;

namespace Tests
{
    [Collection("Database Collection")]
    public class CSScrpitTests
    {
        public DatabaseFixture databaseFixture { get; set; }

        public CSScrpitTests(DatabaseFixture df){
            databaseFixture = df;
        }

        [Fact]
        [Trait("Category", "CSScripts")]
        public void Test1(){
            CScript csScript = new CScript();
            csScript.Test();
            Assert.Equal("a", "a");
        }


        #region Util functions

        #endregion
    }
}

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
    public class LuaTests
    {
        public DatabaseFixture databaseFixture { get; set; }

        public LuaTests(DatabaseFixture df){
            databaseFixture = df;
        }

        [Fact]
        [Trait("Category", "Lua")]
        public void Test1(){

        }


        #region Util functions

        #endregion
    }
}

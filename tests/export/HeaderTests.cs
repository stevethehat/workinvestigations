using System;

using Xunit;
using Xunit.Abstractions;

using Api.Util;

namespace Tests {
    [Collection("Database Collection")]
    public class HeaderTests : RetailTestBase{

        public HeaderTests(DatabaseFixture df)
        {
            _databaseFixture = df;
        }

        [Fact]
        [Trait("Category", "Header")]
        public void Scan() 
        {
        }
    }
}

using System;

using Xunit;
using Xunit.Abstractions;

using Api.Util;

namespace Tests {
    [Collection("Database Collection")]
    public class BandTests : RetailTestBase {


        [Fact]
        [Trait("Category", "Bands")]
        public void CreateBands() 
        {
            BandChanges bands = new BandChanges(97);

            bands.Add(1f);
            bands.Add(47f);
            bands.Add(96f);

            Assert.Equal(bands.Bands.Length, 10);
            Assert.Equal(bands.BandRange, 9.7f);
        }

        private void Test()
        {

        }
    }
}

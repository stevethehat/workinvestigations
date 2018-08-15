using System;

using Xunit;
using Xunit.Abstractions;

using Api.Util;

namespace Tests {
    [Collection("Database Collection")]
    public class BandTests : RetailTestBase {

        public BandTests(DatabaseFixture df)
        {
            _databaseFixture = df;
        }

        [Fact]
        [Trait("Category", "Bands")]
        public void Scan() 
        {
            ClearDatabase();
            AddPair("Test 1", 10, 15);
            AddPair("Test 2", 10, 12);
            AddPair("Test 3", 10, 7);

            RetailChangeSummary summary = new RetailChangeSummary(_databaseFixture.Db, 1, 2, "retail");
            summary.Scan();
            BandChanges bandsUp = new BandChanges(summary.MaxPercentageUp);
            BandChanges bandsDown = new BandChanges(summary.MaxPercentageDown);
            Scanner scanner = new Scanner(_databaseFixture.Db, 1, 2, "retail");
            scanner.Changed = (current, import) =>
            {
                float change = import.Retail - current.Retail;
                float percentageChange = change / current.Retail * 100;

                if(percentageChange > 0) {
                    bandsUp.Add(percentageChange);
                }
                else
                {
                    bandsDown.Add(percentageChange);
                }
                return true;
            };
            scanner.Scan();

            
            //Assert.Equal(bandsUp.Bands.Length, 10);
            //Assert.Equal(bandsUp.BandRange, 5);
        }

        [Fact]
        [Trait("Category", "Bands")]
        public void CreateBands()
        {
            BandChanges bands = new BandChanges(100);


            bands.Add(0f);
            bands.Add(1f);
            bands.Add(49.9f);
            bands.Add(50f);
            bands.Add(100f);

            //Assert.Equal(bands.Bands.Length, 10);
            //Assert.Equal(bands.BandRange, 10f);
            //Assert.Equal(bands.Bands[0], 2);
        }

        [Fact]
        [Trait("Category", "Bands")]
        public void Random()
        {
            int[] values = new int[10];

        }
    }
}

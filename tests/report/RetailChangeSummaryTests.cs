using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using Api.Util;

namespace Tests
{
    [Collection("Database Collection")]

    public class RetailChangeSummaryTests : RetailTestBase {
        public RetailChangeSummaryTests(DatabaseFixture df) {
            databaseFixture = df;
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void EmptyTable(){
            ClearDatabase();

            Test(0, 0, 0);
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void Up1() {
            ClearDatabase();
            AddPair("More Expensive", 10, 20);

            Test(1, 0, 0);
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void Same1() {
            ClearDatabase();
            AddPair("Same", 10, 10);

            Test(0, 1, 0);
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void Down1() {
            ClearDatabase();
            AddPair("Cheaper", 20, 10);

            Test(0, 0, 1);
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void Up50pc() {
            ClearDatabase();
            AddPair("Cheaper", 10, 15);

            Test(1, 0, 0);
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void Down50pc() {
            ClearDatabase();
            AddPair("Cheaper", 10, 5);

            Test(0, 0, 1);
        }

        [Fact]
        [Trait("Category", "RetailChange")]
        public void Down50Bands() {
            ClearDatabase();
            AddPair("Cheaper", 10, 5);

            Test(0, 0, 1);
        }


        private void Test(int up, int same, int down) {
            RetailChangeSummary retailChangeSummary = new RetailChangeSummary(databaseFixture.Db, 1, 2, "retail");

            retailChangeSummary.GetSummary();
            retailChangeSummary.GetPercentageChangeBands();

            Assert.Equal(retailChangeSummary.Down, down);
            Assert.Equal(retailChangeSummary.Same, same);
            Assert.Equal(retailChangeSummary.Up, up);
        }
    }
}

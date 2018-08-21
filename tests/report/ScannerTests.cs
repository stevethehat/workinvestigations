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
    public class ScannerTests: RetailTestBase
    {
        private int changedCount = 0;
        private int unchangedCount = 0;
        private int addedCount = 0;
        private int deletedCount = 0;

        /*
        public ScannerTests(DatabaseFixture df){
            databaseFixture = df;
        }
        */
        
        [Fact]
        [Trait("Category", "Scanner")]
        public void EmptyTable()
        {
            ClearDatabase();
            Scanner scanner = new Scanner(_databaseFixture.Db, 1, 2, "retail");
            scanner.Scan();
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void JustAdded()
        {
            ClearDatabase();
            AddAdded();

            TestCounts(0, 0, 1, 0);
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void JustDeleted()
        {
            ClearDatabase();
            AddDeleted();

            TestCounts(0, 0, 0, 1);
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void AddedAndDeleted()
        {
            ClearDatabase();
            AddDeleted();
            AddAdded();
            AddAdded();
            AddDeleted();
            AddDeleted();
            AddAdded();
            AddDeleted();

            TestCounts(0, 0, 3, 4);
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void Deleted2()
        {
            ClearDatabase();
            AddDeleted();
            AddDeleted();

            TestCounts(0, 0, 0, 2);
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void Deleted3()
        {
            ClearDatabase();
            AddDeleted();
            AddDeleted();
            AddDeleted();

            TestCounts(0, 0, 0, 3);
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void ScannerTest()
        {
            ClearDatabase();
            AddPair("same prices", 10, 10);
            AddPair("changed prices", 10, 12);
            AddAdded();
            AddDeleted();

            TestCounts(1, 1, 1, 1);
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void Random()
        {
            ClearDatabase();
            var added = 0;
            var deleted = 0;
            var changed = 0;
            var unChanged = 0;


            Random random = new Random();
            int total = random.Next(0, 100000);

            for(var i = 0;i < total; i++)
            {
                switch(random.Next(0, 4))
                {
                    case 0:
                        AddAdded();
                        added++;
                        break;
                    case 1:
                        AddDeleted();
                        deleted++;
                        break;
                    case 2:
                        AddPair("Equal pair", 10, 10);
                        unChanged++;
                        break;
                    case 3:
                        AddPair("Equal pair", 10, 12);
                        changed++;
                        break;

                }
            }

            Assert.Equal(total, changed + unChanged + added + deleted);
            TestCounts(changed, unChanged, added, deleted);
        }


        #region Util functions
        private void SetupWatchers(Scanner scanner) {
            scanner.Changed = (current, import) => {
                changedCount++;
                return false;
            };
            scanner.Unchanged = (current, import) => {
                unchangedCount++;
                return false;
            };
            scanner.Added = (item) => {
                addedCount++;
                return false;
            };
            scanner.Deleted = (item) => {
                deletedCount++;
                return false;
            };
        }

        private void TestCounts(int changed, int unchanged, int added, int deleted) {
            Scanner scanner = new Scanner(_databaseFixture.Db, 1, 2, "retail");
            SetupWatchers(scanner);
            scanner.Scan();

            Assert.Equal(changedCount, changed);
            Assert.Equal(unchangedCount, unchanged);
            Assert.Equal(addedCount, added);
            Assert.Equal(deletedCount, deleted);
        }

        #endregion
    }
}

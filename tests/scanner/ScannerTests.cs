using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using System.Data.Common;
using System.Data.SqlClient;
using Dapper;

using Xunit;

using Api.Util;

namespace Tests
{
    [Collection("Database Collection")]
    public class ScannerTests
    {
        public TestServer testServer {get;set;} 
        public DatabaseFixture databaseFixture {get;set;}

        private int import1 = 1;
        private int import2 = 2;
        private int currentPartNumber = 1;

        private int changedCount = 0;
        private int unchangedCount = 0;
        private int addedCount = 0;
        private int deletedCount = 0;

        public ScannerTests(DatabaseFixture df){
            databaseFixture = df;
        }

        [Fact]
        [Trait("Category", "Scanner")]
        public void EmptyTable()
        {
            Scanner scanner = new Scanner(databaseFixture.Db, "retail");
            scanner.Scan(1, 2);
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

        #region Util functions
        private void ClearDatabase()
        {
            DbCommand command = databaseFixture.Db.Connection.CreateCommand();
            command.CommandText = "truncate table prices";

            command.ExecuteNonQuery();
        }
        private void AddRecord(int importId, int partNumber, string description, int retail)
        {
            DbCommand command = databaseFixture.Db.Connection.CreateCommand();
            command.CommandText = $"insert into prices (import_id, partnumber, description, retail_price) values ({importId}, {partNumber}, '{description}', {retail})";

            command.ExecuteNonQuery();
        }
        private void AddAdded()
        {
            AddRecord(import2, currentPartNumber, $"Added {currentPartNumber}", 10);
            currentPartNumber++;
        }

        private void AddDeleted()
        {
            AddRecord(import1, currentPartNumber, $"Deleted {currentPartNumber}", 10);
            currentPartNumber++;
        }

        private void AddPair(string description, int retail1, int retail2)
        {
            AddRecord(import1, currentPartNumber, $"{description} {currentPartNumber}", retail1);
            AddRecord(import2, currentPartNumber, $"{description} {currentPartNumber}", retail2);
            currentPartNumber++;
        }

        private void SetupWatchers(Scanner scanner)
        {
            scanner.Changed = (current, import) => {
                changedCount++;
                return false;
            };
            scanner.Unchanged = (current, import) => {
                unchangedCount++;
                return false;
            };
            scanner.Added = (current, import) => {
                addedCount++;
                return false;
            };
            scanner.Deleted = (current, import) => {
                deletedCount++;
                return false;
            };
        }

        private void TestCounts(int changed, int unchanged, int added, int deleted)
        {
            Scanner scanner = new Scanner(databaseFixture.Db, "retail");
            SetupWatchers(scanner);
            scanner.Scan(1, 2);

            Assert.Equal(changedCount, changed);
            Assert.Equal(unchangedCount, unchanged);
            Assert.Equal(addedCount, added);
            Assert.Equal(deletedCount, deleted);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Data.Common;
using System.Data.SqlClient;

using Xunit;

using Api.Util;


namespace Tests {
    //[Collection("Database Collection")]
    public class RetailTestBase
    {
        public DatabaseFixture databaseFixture { get; set; }

        private int import1 = 1;
        private int import2 = 2;
        private int currentPartNumber = 1;

        protected void ClearDatabase() {
            DbCommand command = databaseFixture.Db.Connection.CreateCommand();
            command.CommandText = "truncate table prices";

            command.ExecuteNonQuery();
        }
        private void AddRecord(int importId, int partNumber, string description, int retail) {
            DbCommand command = databaseFixture.Db.Connection.CreateCommand();
            command.CommandText = $"insert into prices (import_id, partnumber, description, retail) values ({importId}, {partNumber}, '{description}', {retail})";

            command.ExecuteNonQuery();
        }
        protected void AddAdded() {
            AddRecord(import2, currentPartNumber, $"Added {currentPartNumber}", 10);
            currentPartNumber++;
        }

        protected void AddDeleted() {
            AddRecord(import1, currentPartNumber, $"Deleted {currentPartNumber}", 10);
            currentPartNumber++;
        }

        protected void AddPair(string description, int retail1, int retail2) {
            AddRecord(import1, currentPartNumber, $"{description} {currentPartNumber}", retail1);
            AddRecord(import2, currentPartNumber, $"{description} {currentPartNumber}", retail2);
            currentPartNumber++;
        }
    }
}

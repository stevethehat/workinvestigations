using System;
using System.Collections.Generic;

namespace Api.Util {

    public class RecordDetail {
        public string PartNumber { get; set; }
        public int ImportId { get; set; }
        public string Description { get; set; }
        public float Retail { get; set; }

        public string ComparisonField { get; set; }

        public Object[] Values { get; set; }
    }
    //public Func<RecordDetail, RecordDetail, bool>
    public class Scanner {
        public AppDb Db { get; private set; }
        public string CompareField { get; private set; }
        public int CurrentId { get; private set; }
        public int ImportId { get; private set; }
        public int Total { get; private set; }

        private Queue<RecordDetail> records { get; set; }

        private int CompareFieldIndex { get; set; }
        public Func<RecordDetail, bool> Added { private get; set; }
        public Func<RecordDetail, bool> Deleted { private get; set; }
        public Func<RecordDetail, RecordDetail, bool> Unchanged { private get; set; }
        public Func<RecordDetail, RecordDetail, bool> Changed { private get; set; }


        public Scanner(AppDb db, int currentId, int importId, string compareField) {
            Db = db;
            CompareField = compareField;
            CurrentId = currentId;
            ImportId = importId;
        }

        private void ProcessChange(RecordDetail firstItem, RecordDetail secondItem){
            if (firstItem.ComparisonField != secondItem.ComparisonField) {
                if (Changed != null) {
                    Changed(firstItem, secondItem);
                }
            } else {
                if (Unchanged != null) {
                    Unchanged(firstItem, secondItem);
                }
            }
            Total++;
        }

        private void ProcessSingleItem(RecordDetail item) {
            if(item.ImportId == CurrentId) {
                if(Deleted != null) {
                    Deleted(item);
                }
            }
            if(item.ImportId == ImportId) {
                if(Added != null) {
                    Added(item);
                }
            }
            Total++;
        }

        private void ProcessQueue() {
            RecordDetail firstItem;
            RecordDetail secondItem;

            if (records.Count == 0) {
                return;
            }

            firstItem = records.Dequeue();

            if(records.Count == 0) {
                ProcessSingleItem(firstItem);
            } else {
                secondItem = records.Peek();
                if(firstItem.PartNumber == secondItem.PartNumber) {
                    // happy days :)  we have a matching pair!!
                    ProcessChange(firstItem, secondItem);
                    records.Dequeue();
                } else {
                    ProcessSingleItem(firstItem);
                }
            }
        }

        public void Scan() {
            records = new Queue<RecordDetail>();

            RecordDetail rowValues = new RecordDetail();

            var command = Db.Connection.CreateCommand();

            command.CommandText = $"select * from prices where import_id in ({CurrentId}, {ImportId}) order by partnumber, import_id;";
            System.Data.IDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                //currentValues 
                rowValues = new RecordDetail() {
                    PartNumber = reader.GetString(reader.GetOrdinal("partnumber")),
                    ImportId = reader.GetInt32(reader.GetOrdinal("import_id")),
                    Description = reader.GetString(reader.GetOrdinal("description")),
                    Retail = reader.GetFloat(reader.GetOrdinal("retail")),
                    ComparisonField = reader.GetString(reader.GetOrdinal(CompareField))
                };
                //reader.GetValues(rowValues.Values);

                records.Enqueue(rowValues);
                if(records.Count == 2) {
                    ProcessQueue();
                }
            }
            ProcessQueue();
            reader.Close();
        }
    }
}
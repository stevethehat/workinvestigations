using System;
using System.Collections.Generic;

namespace Api.Util {

    public class RecordDetail {
        public string PartNumber { get; set; }
        public int ImportId { get; set; }
        public string Description { get; set; }
        public float Retail { get; set; }
    }
    //public Func<RecordDetail, RecordDetail, bool>
    public class Scanner {
        public AppDb Db { get; private set; }
        public string CompareField { get; private set; }
        public int CurrentId { get; private set; }
        public int ImportId { get; private set; }
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

        /*
        protected void ProcessAddedDeleted(int currentId, int importId, int pass, RecordDetail currentValues, RecordDetail rowValues) {
            if (currentValues.ImportId == importId) {
                // this must have been added
                // added
                if (Added != null) {
                    Added(null, rowValues);
                }
            }
            if (rowValues.ImportId == importId) {
                // this must have been added
                // added
                if (Added != null) {
                    Added(null, rowValues);
                }
            }
            if (currentValues.ImportId == currentId) {
                if (Deleted != null) {
                    Deleted(rowValues, null);
                }
            }
            if (rowValues.ImportId == currentId) {
                if (Deleted != null) {
                    Deleted(rowValues, null);
                }
            }
        }

        */
        private void ProcessChange(RecordDetail firstItem, RecordDetail secondItem){
            if (firstItem.Retail != secondItem.Retail) {
                if (Changed != null) {
                    Changed(firstItem, secondItem);
                }
            } else {
                if (Unchanged != null) {
                    Unchanged(firstItem, secondItem);
                }
            }
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
                    Retail = reader.GetFloat(reader.GetOrdinal("retail_price"))
                };

                records.Enqueue(rowValues);
                if(records.Count == 2) {
                    ProcessQueue();
                }
            }
            ProcessQueue();
            reader.Close();
        }

        /*
        public void _Scan(int currentId, int importId) {
            int pass = 0;

            RecordDetail rowValues = new RecordDetail();
            RecordDetail currentValues = new RecordDetail();
            RecordDetail importValues;

            var command = Db.Connection.CreateCommand();

            command.CommandText = $"select * from prices where import_id in ({currentId}, {importId}) order by partnumber, import_id;";
            System.Data.IDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                //currentValues 
                rowValues = new RecordDetail() {
                    PartNumber = reader.GetString(reader.GetOrdinal("partnumber")),
                    ImportId = reader.GetInt32(reader.GetOrdinal("import_id")),
                    Description = reader.GetString(reader.GetOrdinal("description")),
                    Retail = reader.GetFloat(reader.GetOrdinal("retail_price"))
                };

                if (rowValues.PartNumber != currentValues.PartNumber) {
                    // something has been either added or deleted
                    if (pass == 1) {
                        ProcessAddedDeleted(currentId, importId, pass, currentValues, rowValues);

                        // reset everything
                        pass = 0;
                        currentValues = new RecordDetail();
                        rowValues = new RecordDetail();
                    }
                    currentValues = rowValues;
                    rowValues = new RecordDetail();
                }
                pass++;
                if (pass == 2) {
                    // we have 2 matching part numbers
                    importValues = rowValues;

                    if (currentValues.Retail == rowValues.Retail) {
                        // same
                        if (Unchanged != null) {
                            Unchanged(currentValues, importValues);
                        }
                    } else {
                        if (Changed != null) {
                            Changed(currentValues, rowValues);
                        }
                    }

                    // reset everything
                    pass = 0;
                    currentValues = new RecordDetail();
                    rowValues = new RecordDetail();
                }
            }
            ProcessAddedDeleted(currentId, importId, pass, currentValues, rowValues);
            reader.Close();
        }
        */
    }
}
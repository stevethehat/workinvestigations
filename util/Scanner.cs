using System;

namespace Api.Util{

    public class RecordDetail{
        public string PartNumber {get;set;}
        public int ImportId {get;set;}
        public string Description {get;set;}
        public float Retail {get;set;}
    }
    //public Func<RecordDetail, RecordDetail, bool>
    public class Scanner{
        public AppDb Db {get;private set;}
        public string CompareField {get;private set;}
        private int CompareFieldIndex {get;set;}
        public Func<RecordDetail, RecordDetail, bool> Added {private get;set;}
        public Func<RecordDetail, RecordDetail, bool> Deleted {private get;set;}
        public Func<RecordDetail, RecordDetail, bool> Unchanged {private get;set;}
        public Func<RecordDetail, RecordDetail, bool> Changed {private get;set;}
        public Scanner(AppDb db, string compareField){
            Db = db;
            CompareField = compareField;

        }

        public void Scan(int currentId, int importId){
            int count = 0;
            int currentImport = 0;
            
            RecordDetail rowValues;
            RecordDetail currentValues = new RecordDetail();
            RecordDetail importValues;

            var command = Db.Connection.CreateCommand();
            
            command.CommandText = $"select * from prices where import_id in ({currentId}, {importId}) order by partnumber, import_id;";
            System.Data.IDataReader reader = command.ExecuteReader();

            while(reader.Read()){
                //currentValues 
                rowValues = new RecordDetail(){
                    PartNumber = reader.GetString(reader.GetOrdinal("partnumber")),
                    ImportId = reader.GetInt32(reader.GetOrdinal("import_id")),
                    Description = reader.GetString(reader.GetOrdinal("description")),
                    Retail = reader.GetFloat(reader.GetOrdinal("retail_price"))
                };

                if(rowValues.PartNumber != currentValues.PartNumber){
                    if(count == 1){
                        if(rowValues.ImportId == currentId){
                            // deleted
                            if(Deleted != null){
                                Deleted(new RecordDetail(), new RecordDetail());
                            }
                        } else {
                            // added
                            if(Added != null){
                                Added(new RecordDetail(), new RecordDetail());
                            }
                        }
                        count = 0;
                    }
                    currentValues = rowValues;
                }
                count++;                
                if(count == 2){
                    importValues = rowValues;
                    //costChange = rowValues.Retail - currentValues.Retail;
                    //percentageChange = costChange / currentCost * 100;

                    if(currentValues.Retail == rowValues.Retail){
                        // same
                        if(Unchanged != null){
                            Unchanged(currentValues, importValues);
                        }
                    } else {
                        if(Changed != null){
                            Changed(currentValues, rowValues);
                        }
                    }

                    count = 0;
                }
            }
            if(count == 1){
                if(rowValues.ImportId == currentId){
                    // deleted
                    if(Deleted != null){
                        Deleted(new RecordDetail(), new RecordDetail());
                    }
                } else {
                    // added
                    if(Added != null){
                        Added(new RecordDetail(), new RecordDetail());
                    }
                }
                count = 0;
            }            
            reader.Close();
        }
    }
}
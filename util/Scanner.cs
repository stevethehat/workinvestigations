using System;

namespace Api.Util{

    public class RecordDetail{
        public string PartNumber {get;set;}
    }
    //public Func<RecordDetail, RecordDetail, bool>
    public class Scanner{
        public AppDb Db {get;private set;}
        public Func<RecordDetail, RecordDetail, bool> Added {private get;set;}
        public Func<RecordDetail, RecordDetail, bool> Deleted {private get;set;}
        public Func<RecordDetail, RecordDetail, bool> Unchanged {private get;set;}
        public Func<RecordDetail, RecordDetail, bool> Changed {private get;set;}
        public Scanner(AppDb db){
            Db = db;
        }

        public void Scan(int currentId, int importId){
            float costChange;
            float percentageChange;

            string currentPart = "";
            int currentImport = -1;
            string currentDescription = "";
            float currentCost = -1;
            int count = 0;        

            string rowPart = "";
            int rowImport = -1;
            string rowDescription;
            float rowCost;    

            var command = Db.Connection.CreateCommand();
            
            command.CommandText = $"select * from prices where import_id in ({currentId}, {importId}) order by partnumber, import_id;";
            System.Data.IDataReader reader = command.ExecuteReader();

            while(reader.Read()){
                rowPart = reader.GetString(reader.GetOrdinal("partnumber"));
                rowImport = reader.GetInt32(reader.GetOrdinal("import_id"));
                rowDescription = reader.GetString(reader.GetOrdinal("description"));
                rowCost = reader.GetFloat(reader.GetOrdinal("retail_price"));

                if(rowPart != currentPart){
                    if(count == 1){
                        if(currentImport == 1){
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
                    currentPart = rowPart;
                    currentImport = rowImport;
                    currentDescription = rowDescription;
                    currentCost = rowCost;
                }
                count++;                
                if(count == 2){
                    costChange = rowCost - currentCost;
                    percentageChange = costChange / currentCost * 100;

                    if(currentCost == rowCost){
                        // same
                        if(Unchanged != null){
                            Unchanged(new RecordDetail(), new RecordDetail());
                        }
                    } else {
                        if(Changed != null){
                            Changed(new RecordDetail(), new RecordDetail());
                        }
                    }

                    count = 0;
                }
            }
            if(count == 1){
                if(currentImport == 1){
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
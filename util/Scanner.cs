using System;

namespace Api.Util{

    public class RecordDetail{

    }
    public class Scanner{
        public AppDb Db {get;private set;}
        public Func<RecordDetail, RecordDetail> Added {private get;set;}
        public Func<RecordDetail, RecordDetail> Deleted {private get;set;}
        public Func<RecordDetail, RecordDetail> Equal {private get;set;}
        public Func<RecordDetail, RecordDetail> Changed {private get;set;}
        public Scanner(AppDb db){
            Db = db;
        }
    }
}
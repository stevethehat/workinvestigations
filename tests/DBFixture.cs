using System;
using Xunit;

using Api.Util;

namespace Tests{
    public class DatabaseFixture: IDisposable{
        public AppDb Db {get;set;}

        public DatabaseFixture(){
            Db = new AppDb("server=localhost;Database=priceupdates;Uid=root;Pwd=bh49bb;SslMode=none");
            //Db = new AppDb("server = 192.168.56.4; Database = Price_converter; Uid = root; Pwd = tqrzon; SslMode = none");
        }
        public void Dispose(){

        }
    }

    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection: ICollectionFixture<DatabaseFixture>{

    }
}
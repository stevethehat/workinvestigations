using System;
using Xunit;

using Api.Util;

namespace Tests{
    public class DatabaseFixture: IDisposable{
        public AppDb db {get;set;}

        public DatabaseFixture(){

        }
        public void Dispose(){

        }
    }

    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection: ICollectionFixture<DatabaseFixture>{

    }
}
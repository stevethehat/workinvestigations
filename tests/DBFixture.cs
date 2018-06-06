using System;
using Xunit;

namespace Tests{
    public class DatabaseFixture: IDisposable{

        public void Dispose(){

        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection: ICollectionFixture<DatabaseFixture>{

    }
}
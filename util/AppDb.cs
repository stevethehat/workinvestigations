using System;
using System.Data;
using MySql.Data.MySqlClient;

using System.Data.Common;

namespace Api.Util2{
    public class AppDb: IDisposable{
        public DbConnection Connection;

        public AppDb(string connectionString){
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }
        public AppDb(DbConnection connection){
            Connection = connection;
            Connection.Open();
        }
        public void Dispose(){
            Connection.Close();
        }
    }
}
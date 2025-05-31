using System.Data.SqlTypes;
using System.Runtime.InteropServices.ObjectiveC;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DataBaseConnectionSharedLibrary
{
    public class DapperServices
    {
        private readonly SqlConnectionStringBuilder _connection;

        public  DapperServices(SqlConnectionStringBuilder connection)
        {
            _connection = connection;
        }

        public List<T> Execute<T>(string query,object parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var lst = connection.Query<T>(query,connection).ToList();

        }
    }
}

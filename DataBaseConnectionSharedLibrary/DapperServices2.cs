using System.Data.SqlTypes;
using System.Runtime.InteropServices.ObjectiveC;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataBaseConnectionSharedLibrary
{
    public class DapperServices2 : IDapperServices
    {
        private readonly SqlConnectionStringBuilder _connection;

        public  DapperServices2(IConfiguration configuration)
        {
            _connection = new SqlConnectionStringBuilder (configuration.GetConnectionString("DatabaseConnection"));
        }


        public List<T> Query<T>(string query,object? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var res = connection.Query<T>(query,parameters).ToList();
            connection.Close();
            return res;
        }

        public int Execute(string query,object? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            int res = connection.Execute(query, parameters);
            connection.Close();
            return res;
        }
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace DataBaseConnectionSharedLibrary
{
    public class AdoServices
    {
        SqlConnectionStringBuilder _connection;
        public AdoServices(SqlConnectionStringBuilder connection)
        {
            _connection = connection;
        }

        public List<T> Query<T>(string query,params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            connection.Close();
            var res = JsonConvert.SerializeObject(dt);
            var list = JsonConvert.DeserializeObject<List<T>>(res);
            return list;



        }

        public int Execute(string query,params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            int res = cmd.ExecuteNonQuery();
            connection.Close();
            return res;
        }
    }
}

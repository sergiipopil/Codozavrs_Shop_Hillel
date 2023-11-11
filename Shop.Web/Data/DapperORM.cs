using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ShopWeb.Data
{
    public static class DapperORM
    {
        private static string connectionString = "TEst";
        public static void ExecWithoutReturn(string procedureName, object? param)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(procedureName, param, commandType:CommandType.StoredProcedure);
                connection.Close();
            }
        }
        public static T ExecReturnScalar<T>(string procedureName, object? procedureParams)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<T>(sql: procedureName,
                    param: procedureParams,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                connection.Close();
                return result;
            }
        }
        public static IEnumerable<T> ExecReturnList<T>(string procedureName, object? param)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                connection.Close();
            }
        }
    }
}

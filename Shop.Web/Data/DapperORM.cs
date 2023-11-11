using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Shop.Data;
using Shop.Web;

namespace Shop.Data
{
    public class DapperORM
    {
        
        public void ExecWithoutReturn(string procedureName, object? param)
        {
            using (var connection = StartUp._dbContext.CreateRemoteConnection())
            {
                connection.Open();
                connection.Execute(procedureName, param, commandType:CommandType.StoredProcedure);
                connection.Close();
            }
        }
        public T ExecReturnScalar<T>(string procedureName, object? procedureParams)
        {
            using (var connection = StartUp._dbContext.CreateRemoteConnection())
            {
                connection.Open();
                var result = connection.Query<T>(sql: procedureName,
                    param: procedureParams,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                connection.Close();
                return result;
            }
        }
        public IEnumerable<T> ExecReturnList<T>(string procedureName, object? param)
        {
            using (var connection = StartUp._dbContext.CreateRemoteConnection())
            {
                connection.Open();
                return connection.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
                connection.Close();
            }
        }
    }
}

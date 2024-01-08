using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Shop.Data;
using Shop.Web;

namespace Shop.Data
{
    public class DapperORM
    {
        
        public async Task ExecWithoutReturn(string procedureName, object? param)
        {
            using (var connection = StartConfig._dbContext.CreateRemoteConnection())
            {
                await connection.ExecuteAsync(procedureName, param, commandType:CommandType.StoredProcedure);
            }
        }
        public async Task<T> ExecReturnObject<T>(string procedureName, object? procedureParams)
        {
            using (var connection = StartConfig._dbContext.CreateRemoteConnection())
            {
                var result = await connection.QueryAsync<T>(sql: procedureName,
                    param: procedureParams,
                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
        public async Task<IEnumerable<T>> ExecReturnList<T>(string procedureName, object? param)
        {
            using (var connection = StartConfig._dbContext.CreateRemoteConnection())
            {
                return await connection.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

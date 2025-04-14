using Dapper;
using System.Data;

namespace KKHHandsOnProject.Database.DapperData
{
    public interface IDapperRepository
    {
        Task<int> ExecuteAsync(SqlParameters sqlParameters);
        Task<IEnumerable<T>> QueryAsync<T>(SqlParameters sqlParameters);
        Task<T> QueryFirstOrDefaultAsync<T>(SqlParameters sqlParameters);
    }

    public class DapperRepository : IDapperRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IDbConnection _dbContext;
        public DapperRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            _dbContext = _dapperContext.CreateConnection();
        }

        public async Task<int> ExecuteAsync(SqlParameters sqlParameters)
        {
            var result = await _dbContext.ExecuteAsync(sqlParameters.Query, 
                                                       sqlParameters.Parameters,
                                                       commandType: sqlParameters.CommandType, 
                                                       commandTimeout: sqlParameters.Timeout);
            return (int)result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(SqlParameters sqlParameters)
        {
            var items = await _dbContext.QueryAsync<T>(sqlParameters.Query,
                                                       sqlParameters.Parameters,
                                                       commandType: sqlParameters.CommandType,
                                                       commandTimeout: sqlParameters.Timeout);
            return items;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(SqlParameters sqlParameters)
        {
            var item = await _dbContext.QueryFirstOrDefaultAsync<T>(sqlParameters.Query,
                                                                    sqlParameters.Parameters,
                                                                    commandType: sqlParameters.CommandType,
                                                                    commandTimeout: sqlParameters.Timeout);
            return item!;
        }
    }
}

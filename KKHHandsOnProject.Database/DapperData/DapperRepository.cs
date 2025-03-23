using Dapper;
using System.Data;

namespace KKHHandsOnProject.Database.DapperData
{
    public interface IDapperRepository
    {
        Task<int> ExecuteAsync(string query, object? param = null, CommandType? commandType = CommandType.Text, int? timeout = 300);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object? param = null, CommandType? commandType = CommandType.Text, int? timeout = 300);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object? param = null, CommandType? commandType = CommandType.Text, int? timeout = 300);
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

        public async Task<int> ExecuteAsync(string query, 
                                            object? param = null, 
                                            CommandType? type = CommandType.Text, 
                                            int? timeout = 300)
        {
            var result = await _dbContext.ExecuteAsync(query, param, commandType: type, commandTimeout: timeout);
            return (int)result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, 
                                                        object? param = null, 
                                                        CommandType? type = CommandType.Text, 
                                                        int? timeout = 300)
        {
            var items = await _dbContext.QueryAsync<T>(query, param, commandType: type, commandTimeout: timeout);
            return items;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, 
                                                         object? param = null, 
                                                         CommandType? type = CommandType.Text, 
                                                         int? timeout = 300)
        {
            var item = await _dbContext.QueryFirstOrDefaultAsync<T>(query, param, commandType: type, commandTimeout: timeout);
            return item!;
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace KKHHandsOnProject.Database.DapperData
{
    public class DapperService
    {
        private readonly IDapperRepository _dapperRepository;
        public DapperService(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<T> Query<T>(string query, 
                                object? param = null, 
                                CommandType? type = CommandType.Text, 
                                int? timeout=300)
        {
            var items =  _dapperRepository.QueryAsync<T>(query, param, type, timeout);
            return items.Result.ToList();
        }

        public T QueryFirstOrDefault<T>(string query, 
                                        object? param = null, 
                                        CommandType? type = CommandType.Text, 
                                        int? timeout = 300)
        {
            var item = _dapperRepository.QueryFirstOrDefaultAsync<T>(query, param, type, timeout);
            return item.Result!;
        }

        public int Execute(string query,
                           object? param = null,
                           CommandType? type = CommandType.Text,
                           int? timeout = 300)
        {
            if (_dapperRepository.ExecuteAsync(query, param, type, timeout).IsCanceled) return 0; else return 1;
        }

    }
}

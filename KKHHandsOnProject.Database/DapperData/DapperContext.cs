using Microsoft.Data.SqlClient;
using System.Data;

namespace KKHHandsOnProject.Database.DapperData
{
    public class DapperContext
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public DapperContext(string connectionString)
        {
            _connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }
        public IDbConnection CreateConnection()
        {
            IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            return db;
        }
    }
}

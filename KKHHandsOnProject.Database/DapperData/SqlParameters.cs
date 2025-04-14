using System.Data;

namespace KKHHandsOnProject.Database.DapperData
{
    public class SqlParameters
    {
        public string Query { get; set; }
        public object Parameters { get; set; } = null;
        public IDbTransaction transaction { get; set; } = null;
        public int Timeout { get; set; } = 0;
        public CommandType CommandType { get; set; } = CommandType.Text;

    }
}

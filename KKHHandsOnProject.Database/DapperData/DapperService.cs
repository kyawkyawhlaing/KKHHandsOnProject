namespace KKHHandsOnProject.Database.DapperData
{
    public class DapperService
    {
        private readonly IDapperRepository _dapperRepository;
        public DapperService(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<T> Query<T>(SqlParameters sqlParameters)
        {
            var items =  _dapperRepository.QueryAsync<T>(sqlParameters);
            return items.Result.ToList();
        }

        public T QueryFirstOrDefault<T>(SqlParameters sqlParameters)
        {
            var item = _dapperRepository.QueryFirstOrDefaultAsync<T>(sqlParameters);
            return item.Result!;
        }

        public int Execute(SqlParameters sqlParameters)
        {
            if (_dapperRepository.ExecuteAsync(sqlParameters).IsCanceled) return 0; else return 1;
        }

    }
}

namespace KKHHandsOnProject.BlogMvcApp.src.Shared.Models
{
    // Pagination for jQuery DataTable
    public class PaginationModel<T>
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<T> Data { get; set; }
    }
}

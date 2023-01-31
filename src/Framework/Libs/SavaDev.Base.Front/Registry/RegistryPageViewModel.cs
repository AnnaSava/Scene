namespace SavaDev.Base.Front.Registry
{
    public class RegistryPageViewModel<TModel>
    {
        public List<TModel> Items { get; }

        public int Page { get; }

        public int TotalPages { get; }

        public long TotalRows { get; }

        public RegistryPageViewModel(IEnumerable<TModel> mappedItems, int page, int totalPages, long totalRows)
        {
            Items = mappedItems.ToList();
            Page = page;
            TotalPages = totalPages;
            TotalRows = totalRows;
        }
    }
}
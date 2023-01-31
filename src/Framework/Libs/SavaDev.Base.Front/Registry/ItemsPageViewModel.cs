namespace SavaDev.Base.Front.Registry
{
    // TODO пока идентичен RegistryPageViewModel, но может обнаружиться своя специфика. Этот тип для не-реестров (т.е. где нет гибкой настройки)
    public class ItemsPageViewModel<TModel>
    {
        public List<TModel> Items { get; }

        public int Page { get; }

        public int TotalPages { get; }

        public long TotalRows { get; }

        public ItemsPageViewModel(IEnumerable<TModel> mappedItems, int page, int totalPages, long totalRows)
        {
            Items = mappedItems.ToList();
            Page = page;
            TotalPages = totalPages;
            TotalRows = totalRows;
        }
    }
}
using Framework.Base.Service.ListView;
using MudBlazor;

namespace Scene.Manage.UI.MudBlazorServer.Helpers
{
    public static class PageHelper
    {
        public static ListPageInfoViewModel GetPageInfo(TableState state)
        {
            return new ListPageInfoViewModel
            {
                Page = state.Page + 1,
                Rows = state.PageSize,
                Sort = state.SortDirection == SortDirection.None ? null : state.SortDirection == SortDirection.Descending ? "-" + state.SortLabel : state.SortLabel
            };
        }

        public static TableData<T> GetTableData<T>(ListPageViewModel<T> response)
        {
            return new TableData<T> { TotalItems = (int)response.TotalRows, Items = response.Items };
        }
    }
}

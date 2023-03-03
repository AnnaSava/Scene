using Framework.Base.Service.ListView;
using MudBlazor;
using SavaDev.Base.Data.Registry;

namespace Scene.Manage.MudBlazorServer.Helpers
{
    public static class PageHelper
    {
        public static RegistryPageInfo GetPageInfo(TableState state)
        {
            return new RegistryPageInfo
            {
                //Page = state.Page + 1,
                //Rows = state.PageSize,
                //Sort = state.SortDirection == SortDirection.None ? null : state.SortDirection == SortDirection.Descending ? "-" + state.SortLabel : state.SortLabel
            };
        }

        public static TableData<T> GetTableData<T>(ListPageViewModel<T> response)
        {
            return new TableData<T> { TotalItems = (int)response.TotalRows, Items = response.Items };
        }
    }
}

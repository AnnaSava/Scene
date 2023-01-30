using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Types.Registry
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

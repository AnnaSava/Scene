using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Base.Service.ListView
{
    public class PageInfoTypeConverter : ITypeConverter<ListPageInfoViewModel, PageInfoModel>
    {
        public PageInfoModel Convert(ListPageInfoViewModel source, PageInfoModel destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            destination = new PageInfoModel() { PageNumber = source.Page, RowsCount = source.Rows };

            destination.Sort = source.Sort
                  .ToSortDictionary()?
                  .Select((m, i) => new ListSortModel() { FieldName = m.Key, Direction = m.Value, Initial = i == 0 });

            return destination;
        }
    }
}

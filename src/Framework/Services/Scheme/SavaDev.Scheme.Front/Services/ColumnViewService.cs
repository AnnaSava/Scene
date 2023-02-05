using AutoMapper;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Front.Registry;
using SavaDev.Scheme.Data.Contract;
using SavaDev.Scheme.Front.Contract;
using SavaDev.Scheme.Front.Contract.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Services
{
    public class ColumnViewService : IColumnViewService
    {
        protected readonly IColumnService _columnService;
        protected readonly IMapper _mapper;

        public ColumnViewService(IColumnService columnService, IMapper mapper)
        {
            _columnService = columnService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ColumnViewModel>> GetAll(ModelPlacement modelPlacement)
        {
            var list = await _columnService.GetAll(modelPlacement);
            var vw = list.Select(m => _mapper.Map<ColumnViewModel>(m));
            return vw;
        }
    }
}

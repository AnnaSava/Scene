using AutoMapper;
using Savadev.Content.Data.Contract;
using SavaDev.Content.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Services
{
    public class ExportService: IExportService
    {
        private readonly IMapper _mapper;
        private readonly ContentContext _dbContext;

        public ExportService(ContentContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}

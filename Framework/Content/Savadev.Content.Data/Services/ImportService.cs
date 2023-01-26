﻿using AutoMapper;
using Savadev.Content.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savadev.Content.Data.Services
{
    public class ImportService: IImportService
    {
        private readonly IMapper _mapper;
        private readonly ContentContext _dbContext;

        public ImportService(ContentContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}

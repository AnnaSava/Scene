using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services
{
    public class ServiceInftrastructure
    {
        public string ServiceName { get; }

        public IDbContext DbContext { get; }

        public IMapper Mapper { get; }

        public ILogger Logger { get; }

        public ServiceInftrastructure(string serviceName, IDbContext dbContext, IMapper mapper, ILogger logger)
        {
            ServiceName = serviceName;
            DbContext = dbContext;
            Mapper = mapper;
            Logger = logger;
        }
    }
}

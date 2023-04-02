using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Exceptions;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Services
{
    public abstract class BaseEntityService<TEntity, TModel> : IEntityRegistryService
        where TEntity : class, IAnyEntity
        where TModel : IAnyModel
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;
        private readonly string serviceName;
        protected readonly ILogger _logger;

        public BaseEntityService([NotNull] IDbContext dbContext, [NotNull] IMapper mapper, [NotNull] string serviceName)
        {
            // TODO проверить, как работают атрибуты, и убрать бросание ошибок
            _dbContext = dbContext;// ?? throw new DataArgumentException(ExMessageTemplate.ConstructorNullArgument(nameof(dbContext), nameof(IDbContext), serviceName));
            _mapper = mapper;// ?? throw new DataArgumentException(ExMessageTemplate.ConstructorNullArgument(nameof(mapper), nameof(IMapper), serviceName));
        }

        public BaseEntityService([NotNull] IDbContext dbContext, [NotNull] IMapper mapper, [NotNull] string serviceName, ILogger logger)
        {
            // TODO проверить, как работают атрибуты, и убрать бросание ошибок
            _dbContext = dbContext;// ?? throw new DataArgumentException(ExMessageTemplate.ConstructorNullArgument(nameof(dbContext), nameof(IDbContext), serviceName));
            _mapper = mapper;// ?? throw new DataArgumentException(ExMessageTemplate.ConstructorNullArgument(nameof(mapper), nameof(IMapper), serviceName));
            _logger = logger;
        }

        protected ServiceInftrastructure GetInftrastructure => new ServiceInftrastructure(serviceName, _dbContext, _mapper, _logger);
    }
}

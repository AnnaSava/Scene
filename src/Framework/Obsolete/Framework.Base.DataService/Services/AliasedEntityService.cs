using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Entities;
using Framework.Base.Types.ModelTypes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Framework.Base.DataService.Services
{
    public class AliasedEntityService<TEntity, TModel> : RestorableEntityService<TEntity, TModel>, IAliasedEntityService<TModel>
        where TEntity : class, IEntityRestorable, IEntityAliased, IEntity<long>
        where TModel : IModelRestorable, IModelAliased
    {
        public AliasedEntityService(IDbContext dbContext, IMapper mapper, string serviceName)
            : base(dbContext, mapper, serviceName)
        {

        }

        public async Task<TModel> GetOneByAlias(string alias)
        {
            return await _dbContext.GetOneByAlias<TEntity, TModel>(alias, _mapper);
        }
    }
}

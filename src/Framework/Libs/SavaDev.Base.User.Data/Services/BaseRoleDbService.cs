using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SavaDev.Base.User.Data.Adapters.Interfaces;
using SavaDev.Base.User.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Models.Interfaces;

namespace SavaDev.Base.User.Data.Services
{
    public abstract class BaseRoleDbService<TRoleEntity, TRoleClaimEntity, TRoleModel>
        where TRoleEntity : BaseRole
        where TRoleClaimEntity: IdentityRoleClaim<long>, new()
        where TRoleModel : BaseRoleModel
    {
        protected readonly IRoleContext<TRoleEntity, TRoleClaimEntity> _dbContext;
        private readonly IRoleManagerAdapter<TRoleEntity> _roleManagerAdapter;
        protected readonly IMapper _mapper;

        public BaseRoleDbService(
            IRoleContext<TRoleEntity, TRoleClaimEntity> dbContext,
            IRoleManagerAdapter<TRoleEntity> roleManagerAdapter,
            IMapper mapper,
            string serviceName
            )
        {
            _dbContext = dbContext;

            _roleManagerAdapter = roleManagerAdapter;

            _mapper = mapper;
        }

        
    }
}

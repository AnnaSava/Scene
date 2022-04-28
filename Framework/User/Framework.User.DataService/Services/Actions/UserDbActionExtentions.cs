using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Services;
using Framework.Base.Types.DataStorage;
using Framework.Base.Types.ModelTypes;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    public static class UserDbActionExtentions
    {
        public static async Task<TUserModelOut> CreateUser<TUserEntity, TUserModelIn, TUserModelOut>(this IUserManagerAdapter<TUserEntity> adapter, 
            TUserModelIn model, 
            string password, 
            IMapper mapper)
            where TUserEntity : BaseUser
        {
            var entity = mapper.Map<TUserEntity>(model);
            var created = await adapter.CreateAsync(entity, password);

            return mapper.Map<TUserModelOut>(created);
        }

        public static async Task<TUserModelOut> UpdateUser<TUserEntity,TUserModelIn, TUserModelOut >(this IUserManagerAdapter<TUserEntity> adapter,
            TUserModelIn model,
            IDbContext dbContext,
            IMapper mapper)
            where TUserEntity : BaseUser
            where TUserModelIn : IModel<long>
        {
            var currentEntity = await dbContext.GetEntityForUpdate<TUserEntity>(model.Id);

            mapper.Map(model, currentEntity);
            var updated = await adapter.UpdateAsync(currentEntity);

            return mapper.Map<TUserModelOut>(updated);
        }

        public static async Task<TUserModel> Delete<TUserEntity, TUserModel>(this IUserManagerAdapter<TUserEntity> adapter,
            long id,
            IDbContext dbContext,
            IMapper mapper)
            where TUserEntity : BaseUser
        {
            var entity = await dbContext.GetEntityForUpdate<TUserEntity>(id);
            if (entity.UserName == "admin") throw new InvalidOperationException("Removing of user admin is forbidden!");

            entity.IsDeleted = true;
            var deleted = await adapter.UpdateAsync(entity);

            return mapper.Map<TUserModel>(deleted);
        }

        public static async Task<TUserModel> Restore<TUserEntity, TUserModel>(this IUserManagerAdapter<TUserEntity> adapter,
            long id,
            IDbContext dbContext,
            IMapper mapper)
            where TUserEntity : BaseUser
        {
            var entity = await dbContext.GetEntityForRestore<TUserEntity>(id);
            entity.IsDeleted = false;
            var deleted = await adapter.UpdateAsync(entity);

            return mapper.Map<TUserModel>(deleted);
        }

        public static async Task<TUserModel> GetUserByLogin<TUserEntity, TUserModel>(this IDbContext dbContext, string login, IMapper mapper)
            where TUserEntity : BaseUser
        {
            var entity = await dbContext.Set<TUserEntity>().FirstOrDefaultAsync(m => m.UserName == login && m.IsDeleted == false);
            return mapper.Map<TUserModel>(entity);
        }

        public static async Task<TUserModel> GetUserByEmail<TUserEntity, TUserModel>(this IDbContext dbContext, string email, IMapper mapper)
            where TUserEntity : BaseUser
        {
            var entity = await dbContext.Set<TUserEntity>().FirstOrDefaultAsync(m => m.Email == email && m.IsDeleted == false);
            return mapper.Map<TUserModel>(entity);
        }

        public static async Task<bool> CheckUserEmailExists<TUserEntity>(this IDbContext dbContext, string email)
            where TUserEntity : BaseUser
        {
            if (string.IsNullOrEmpty(email)) return false;
            return await dbContext.Set<TUserEntity>().AnyAsync(m => m.NormalizedEmail == email.ToUpper());
        }

        public static async Task<bool> CheckUserLoginExists<TUserEntity>(this IDbContext dbContext, string login)
            where TUserEntity : BaseUser
        {
            if (string.IsNullOrEmpty(login)) return false;
            return await dbContext.Set<TUserEntity>().AnyAsync(m => m.NormalizedUserName == login.ToUpper());
        }
    }
}

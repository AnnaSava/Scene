using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Services;
using SavaDev.Base.User.Data.Context;
using SavaDev.Base.User.Data.Entities;
using SavaDev.Base.User.Data.Manager;
using SavaDev.Base.User.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Services
{
    public class BaseUserDbService<TEntity, TFormModel>
        where TEntity : BaseUser
        where TFormModel : BaseUserModel
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;
        private readonly UserManager<TEntity> _userManager;

        protected readonly UserEntityManager<long, TEntity, TFormModel> entityManager;

        public BaseUserDbService(
            IDbContext dbContext,
            UserManager<TEntity> userManager,
            IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;

            entityManager = new UserEntityManager<long, TEntity, TFormModel>(dbContext, _userManager, mapper, logger);
        }

        public async Task<OperationResult<TFormModel>> Create(TFormModel model, string password)
            => await entityManager.Create(model, password);
        public async Task<OperationResult<TFormModel>> Update(long id, TFormModel model)
            => await entityManager.Update(id, model);
        public async Task<OperationResult> Delete(long id)
             => await entityManager.Delete(id);
        public async Task<OperationResult> Restore(long id)
             => await entityManager.Restore(id);


        public async Task<OperationResult<TModel>> Lock<TModel>(UserLockoutModel lockoutModel)
            => await entityManager.Lock<TModel>(lockoutModel);
        public async Task<OperationResult<TModel>> Unlock<TModel>(long id)
            => await entityManager.Unlock<TModel>(id);
        public async Task<OperationResult> UpdateRoles(UserRolesModel model)
            => await entityManager.UpdateRoles(model);
        public async Task<OperationResult> AddRoles(UserRolesModel model)
            => await entityManager.AddRoles(model);
        public async Task<OperationResult> RemoveRoles(UserRolesModel model)
            => await entityManager.RemoveRoles(model);


        public async Task<TModel> GetOneByLoginOrEmail<TModel>(string loginOrEmail) where TModel : BaseUserModel
            => await entityManager.GetOneByLoginOrEmail<TModel>(loginOrEmail);
        public async Task<TModel> GetOneByLogin<TModel>(string login)
            => await entityManager.GetOneByLogin<TModel>(login);
        public async Task<TModel> GetOneByEmail<TModel>(string email)
            => await entityManager.GetOneByEmail<TModel>(email);
        public async Task<bool> CheckEmailExists(string email)
            => await entityManager.CheckEmailExists(email);
        public async Task<bool> CheckLoginExists(string login)
            => await entityManager.CheckLoginExists(login);
        public async Task<bool> IsLocked(string id)
            => await entityManager.IsLocked(id);

        public async Task<IEnumerable<TModel>> GetAllByIds<TModel>(IEnumerable<string> ids) => await entityManager.GetAllByIds<TModel>(ids);


        // TODO подумать, куда перенести, т.к. в теории может пригодиться не только для пользователей
        //protected IEnumerable<ListSortModel> GetChangedSortFields(IEnumerable<ListSortModel> sortList, Dictionary<string, string> diff)
        //{
        //    if (sortList == null) return null;

        //    var newSortList = new List<ListSortModel>();

        //    foreach (var sort in sortList)
        //    {
        //        var newSort = sort;
        //        var fieldName = sort.FieldName.ToLower();
        //        if (diff.ContainsKey(fieldName))
        //        {
        //            newSort.FieldName = diff[fieldName];
        //        }

        //        newSortList.Add(newSort);
        //    }
        //    return newSortList;
        //}
    }
}

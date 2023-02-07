using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Data.Services;
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
        #region Protected Fields: Dependencies

        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly UserManager<TEntity> _userManager;

        #endregion

        #region Protected Properties: Managers

        protected UserEntityManager<long, TEntity, TFormModel> EntityManager { get; }

        #endregion

        #region Public Constructors

        public BaseUserDbService(
            IDbContext dbContext,
            UserManager<TEntity> userManager,
            IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            EntityManager = new UserEntityManager<long, TEntity, TFormModel>(dbContext, _userManager, mapper, logger);
        }

        #endregion

        #region Public Methods: Mutation

        public async Task<OperationResult<TFormModel>> Create(TFormModel model, string password)
            => await EntityManager.Create(model, password);
        public async Task<OperationResult<TFormModel>> Update(long id, TFormModel model)
            => await EntityManager.Update(id, model);
        public async Task<OperationResult> Delete(long id)
             => await EntityManager.Delete(id);
        public async Task<OperationResult> Restore(long id)
             => await EntityManager.Restore(id);

        public async Task<OperationResult<TModel>> Lock<TModel>(UserLockoutModel lockoutModel)
            => await EntityManager.Lock<TModel>(lockoutModel);
        public async Task<OperationResult<TModel>> Unlock<TModel>(long id)
            => await EntityManager.Unlock<TModel>(id);
        public async Task<OperationResult> UpdateRoles(UserRolesModel model)
            => await EntityManager.UpdateRoles(model);
        public async Task<OperationResult> AddRoles(UserRolesModel model)
            => await EntityManager.AddRoles(model);
        public async Task<OperationResult> RemoveRoles(UserRolesModel model)
            => await EntityManager.RemoveRoles(model);

        #endregion

        #region Public Methods^ Query One

        public async Task<TModel> GetOneByLoginOrEmail<TModel>(string loginOrEmail) where TModel : BaseUserModel
            => await EntityManager.GetOneByLoginOrEmail<TModel>(loginOrEmail);
        public async Task<TModel> GetOneByLogin<TModel>(string login)
            => await EntityManager.GetOneByLogin<TModel>(login);
        public async Task<TModel> GetOneByEmail<TModel>(string email)
            => await EntityManager.GetOneByEmail<TModel>(email);
        public async Task<bool> CheckEmailExists(string email)
            => await EntityManager.CheckEmailExists(email);
        public async Task<bool> CheckLoginExists(string login)
            => await EntityManager.CheckLoginExists(login);
        public async Task<bool> IsLocked(string id)
            => await EntityManager.IsLocked(id);

        #endregion

        public async Task<IEnumerable<TModel>> GetAllByIds<TModel>(IEnumerable<string> ids) => await EntityManager.GetAllByIds<TModel>(ids);


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

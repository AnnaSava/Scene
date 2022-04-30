using AutoMapper;
using Framework.Base.Exceptions;
using Framework.Base.Types.ModelTypes;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Services
{
    // TODO Подумать о вынесении в отдельную сборку
    public class UserDbService<TUserEntity>
        where TUserEntity : BaseUser
    {
        protected readonly IUserContext<TUserEntity> _dbContext;
        protected IUserManagerAdapter<TUserEntity> _userManagerAdapter;
        protected readonly IMapper _mapper;

        public UserDbService(IUserContext<TUserEntity> dbContext, IUserManagerAdapter<TUserEntity> userManagerAdapter, IMapper mapper)
        {
            _dbContext = dbContext;

            _userManagerAdapter = userManagerAdapter;

            _mapper = mapper ?? throw new ProjectArgumentException(
                GetType(),
                nameof(UserDbService<TUserEntity>),
                nameof(mapper),
                null);
        }

        public async Task<TUserModel> Create<TUserModel>(TUserModel model, string password)
        {
            return await _userManagerAdapter.CreateUser<TUserEntity, TUserModel, TUserModel>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Create<TUserModelIn, TUserModelOut>(TUserModelIn model, string password)
        {
            return await _userManagerAdapter.CreateUser<TUserEntity, TUserModelIn, TUserModelOut>(model, password, _mapper);
        }

        public async Task<TUserModelOut> Update<TUserModelIn, TUserModelOut>(TUserModelIn model)
            where TUserModelIn : IModel<long>
        {
            return await _userManagerAdapter.UpdateUser<TUserEntity, TUserModelIn, TUserModelOut>(model, _dbContext, _mapper);
        }

        public async Task<TUserModelOut> Delete<TUserModelOut>(long id)
        {
            return await _userManagerAdapter.Delete<TUserEntity, TUserModelOut>(id, _dbContext, _mapper);
        }

        public async Task<TUserModelOut> Restore<TUserModelOut>(long id)
        {
            return await _userManagerAdapter.Restore<TUserEntity, TUserModelOut>(id, _dbContext, _mapper);
        }
    }
}

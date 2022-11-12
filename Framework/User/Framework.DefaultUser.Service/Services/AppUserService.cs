using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Framework.Base.Types.ModelTypes;
using Framework.Base.Types.Validation;
using Framework.DefaultUser.Data.Contract;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Framework.User.Service.Taskers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    public class AppUserService : BaseUserService, IAppFrameworkUserService
    {
        private const string DefaultUserViewTarget = "";
        private const string FormUserViewTarget = "form";

        private readonly IAppUserDbService _userDbService;
        private readonly IAppAccountDbService _accountDbService;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        private readonly IReservedNameDbService _reservedNameDbService;
        private readonly RegisterTasker _registerTasker;
        private readonly IMapper _mapper;

        public AppUserService(IAppUserDbService userDbService,
            IAppAccountDbService accountDbService,
            ISignInManagerAdapter signInManagerAdapter,
            IReservedNameDbService reservedNameDbService,
            RegisterTasker registerTasker,
            IMapper mapper)
        {
            _userDbService = userDbService;
            _accountDbService = accountDbService;
            _signInManagerAdapter = signInManagerAdapter;
            _reservedNameDbService = reservedNameDbService;
            _registerTasker = registerTasker;
            _mapper = mapper;
        }

        public async Task<AppUserViewModel> Create(AppUserFormViewModel model, string password)
        {
            var newModel = _mapper.Map<AppUserFormModel>(model);
            var resultModel = await _userDbService.Create<AppUserModel>(newModel, password);
            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<AppUserViewModel> Update(AppUserFormViewModel model)
        {
            var newModel = _mapper.Map<AppUserFormModel>(model);
            var resultModel = await _userDbService.Update<AppUserModel>(newModel);
            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<AppUserViewModel> Delete(long id)
        {
            var resultModel = await _userDbService.Delete<AppUserModel>(id);
            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<AppUserViewModel> Restore(long id)
        {
            var resultModel = await _userDbService.Restore<AppUserModel>(id);
            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<AppUserViewModel> Lock(UserLockoutViewModel lockoutModel)
        {
            var end = DateTimeOffset.TryParse(lockoutModel.LockoutEnd, out DateTimeOffset res);

            var model = new UserLockoutModel() //TODO сделать маппер с разбором даты
            {
                Id = lockoutModel.Id,
                LockoutEnd = end ? res : null,
                Reason = lockoutModel.Reason
            };

            var resultModel = await _userDbService.Lock<AppUserViewModel>(model);
            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<AppUserViewModel> Unlock(long id)
        {
            var resultModel = await _userDbService.Unlock<AppUserViewModel>(id);
            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<AppUserViewModel>> GetAll(AppUserFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<AppUserFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _userDbService.GetAll(new ListQueryModel<AppUserFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = new ListPageViewModel<AppUserViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<AppUserModel, AppUserViewModel>(m)),
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }

        public async Task<IAppUserViewModel> GetOne(long id, string target)
        {
            string include = null;
            // TODO придумать, как разруливать таргеты и типы, в т.ч. на уровне датасервиса

            //  var model = await _userDbService.GetOne(id);

            if (target == null) target = string.Empty;

            var types = new Dictionary<string, Func<long, string, Task<IAppUserViewModel>>>()
            {
                { DefaultUserViewTarget, GetOneForView },
                { FormUserViewTarget, GetOneForForm }
            };

            return await types[target](id, include);
        }

        private async Task<IAppUserViewModel> GetOneForForm(long id, string include)
        {
            var model = await _userDbService.GetOne<AppUserFormModel>(id, include);
            return _mapper.Map<AppUserFormViewModel>(model);
        }

        private async Task<IAppUserViewModel> GetOneForView(long id, string include)
        {
            var model = await _userDbService.GetOne<AppUserModel>(id, include);
            return _mapper.Map<AppUserViewModel>(model);
        }

        public async Task<AppUserViewModel> GetOneByEmail(string email)
        {
            var model = await _userDbService.GetOneByEmail<AppUserModel>(email);
            return _mapper.Map<AppUserViewModel>(model);
        }

        public async Task<Dictionary<string, bool>> CheckExists(string email, string login)
        {
            var dict = new Dictionary<string, bool>();

            if (!string.IsNullOrEmpty(email))
                dict.Add(nameof(email), await _userDbService.CheckEmailExists(email));

            if (!string.IsNullOrEmpty(login))
                dict.Add(nameof(login), await _userDbService.CheckUserNameExists(login));

            return dict;
        }

        public async Task<FieldValidationResult> ValidateField(string name, string value)
        {
            if (name == "login")
            {
                var text = value;

                if (!string.IsNullOrEmpty(text))
                {
                    var result = new FieldValidationResult(name);
                    var reserved = await _reservedNameDbService.CheckIsReserved(text);
                    result.IsValid = !reserved;

                    if (reserved)
                    {
                        result.Messages.Add($"User login {text} is reserved");
                        return result;
                    }

                    var exists = await _userDbService.CheckUserNameExists(text);
                    if (exists) result.Messages.Add($"User with login {text} already exists");

                    result.IsValid = !exists;
                    return result;
                }
            }
            else if (name == "email")
            {
                var result = new FieldValidationResult(name);
                var exists = await _userDbService.CheckEmailExists(value);
                if (exists) result.Messages.Add($"User with email {value} already exists");

                result.IsValid = !exists;
                return result;
            }
            else if (name == "phoneNumber")
            {

            }
            return null;
        }

        public async Task AddRoles(long id, UserRolesFormViewModel form)
        {
            var model = _mapper.Map<UserRolesModel>(form);
            model.UserId = id;
            await _userDbService.AddRoles(model);
        }

        public async Task RemoveRoles(long id, UserRolesFormViewModel form)
        {
            var model = _mapper.Map<UserRolesModel>(form);
            model.UserId = id;
            await _userDbService.RemoveRoles(model);
        }
    }
}

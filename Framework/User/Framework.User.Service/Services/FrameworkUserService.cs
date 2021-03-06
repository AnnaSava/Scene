using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Framework.Base.Types.Validation;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    public class FrameworkUserService : IFrameworkUserService
    {
        private const string DefaultUserViewTarget = "";
        private const string FormUserViewTarget = "form";

        private readonly IFrameworkUserDbService _userDbService;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        private readonly IReservedNameDbService _reservedNameDbService;
        private readonly IMapper _mapper;

        public FrameworkUserService(IFrameworkUserDbService userDbService,
            ISignInManagerAdapter signInManagerAdapter,
            IReservedNameDbService reservedNameDbService,
            IMapper mapper)
        {
            _userDbService = userDbService;
            _signInManagerAdapter = signInManagerAdapter;
            _reservedNameDbService = reservedNameDbService;
            _mapper = mapper;
        }

        public async Task<FrameworkUserViewModel> Create(FrameworkUserFormViewModel model, string password)
        {
            var newModel = _mapper.Map<FrameworkUserFormModel>(model);
            var resultModel = await _userDbService.Create<FrameworkUserModel>(newModel, password);
            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<FrameworkUserViewModel> Update(FrameworkUserFormViewModel model)
        {
            var newModel = _mapper.Map<FrameworkUserFormModel>(model);
            var resultModel = await _userDbService.Update<FrameworkUserModel>(newModel);
            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<FrameworkUserViewModel> Delete(long id)
        {
            var resultModel = await _userDbService.Delete<FrameworkUserModel>(id);
            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<FrameworkUserViewModel> Restore(long id)
        {
            var resultModel = await _userDbService.Restore<FrameworkUserModel>(id);
            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<FrameworkUserViewModel> Lock(UserLockoutViewModel lockoutModel)
        {
            var end = DateTimeOffset.TryParse(lockoutModel.LockoutEnd, out DateTimeOffset res);

            var model = new UserLockoutModel() //TODO сделать маппер с разбором даты
            {
                Id = lockoutModel.Id,
                LockoutEnd = end ? res : null,
                Reason = lockoutModel.Reason
            };

            var resultModel = await _userDbService.Lock(model);
            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<FrameworkUserViewModel> Unlock(long id)
        {
            var resultModel = await _userDbService.Unlock(id);
            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<FrameworkUserViewModel>> GetAll(FrameworkUserFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<FrameworkUserFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _userDbService.GetAll(new ListQueryModel<FrameworkUserFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = new ListPageViewModel<FrameworkUserViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<FrameworkUserModel, FrameworkUserViewModel>(m)),
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }

        public async Task<IFrameworkUserViewModel> GetOne(long id, string target)
        {
            string include = null;
            // TODO придумать, как разруливать таргеты и типы, в т.ч. на уровне датасервиса

            //  var model = await _userDbService.GetOne(id);

            if (target == null) target = string.Empty;

            var types = new Dictionary<string, Func<long, string, Task<IFrameworkUserViewModel>>>()
            {
                { DefaultUserViewTarget, GetOneForView },
                { FormUserViewTarget, GetOneForForm }
            };

            return await types[target](id, include);
        }

        public async Task<FrameworkUserViewModel> Register(FrameworkRegisterViewModel model)
        {
            // TODO переделать на возврат списка ошибок?
            if (await _userDbService.CheckEmailExists(model.Email))
                throw new Exception("Email exists!");

            if (await _userDbService.CheckUserNameExists(model.Login)) // TODO: может проверять одним запросом к БД?
                throw new Exception("Username exists!");

            if (await _reservedNameDbService.CheckIsReserved(model.Login))
                throw new Exception("UserName is forbidden!");

            var newModel = _mapper.Map<FrameworkUserFormModel>(model);
            var resultModel = await _userDbService.Create<FrameworkUserModel>(newModel, model.Password);

            if (resultModel == null || resultModel.Id == 0)
                throw new Exception("Registration error");

            // TODO отправлять письмо с подтверждением

            return _mapper.Map<FrameworkUserViewModel>(resultModel);
        }

        public async Task<SignInResult> SignIn(string identifier, string password, bool rememberMe)
        {
            return await _signInManagerAdapter.SignIn(identifier, password, rememberMe);
        }

        public async Task SignOut()
        {
            await _signInManagerAdapter.SignOut();
        }

        private async Task<IFrameworkUserViewModel> GetOneForForm(long id, string include)
        {
            var model = await _userDbService.GetOne<FrameworkUserFormModel>(id, include);
            return _mapper.Map<FrameworkUserFormViewModel>(model);
        }

        private async Task<IFrameworkUserViewModel> GetOneForView(long id, string include)
        {
            var model = await _userDbService.GetOne<FrameworkUserModel>(id, include);
            return _mapper.Map<FrameworkUserViewModel>(model);
        }

        public async Task<FrameworkUserViewModel> GetOneByEmail(string email)
        {
            var model = await _userDbService.GetOneByEmail(email);
            return _mapper.Map<FrameworkUserViewModel>(model);
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

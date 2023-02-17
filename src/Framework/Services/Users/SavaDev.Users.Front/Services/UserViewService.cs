using AutoMapper;
using Framework.Base.Service.ListView;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Interfaces;
using Sava.Users.Front.Contract.Models;
using SavaDev.Base.Data.Models;
using SavaDev.Base.User.Data.Services;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.System.Data.Contract;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Contract.Models;
using SavaDev.Users.Front.Contract.Models;

namespace Framework.User.Service.Services
{
    public class UserViewService //: IUserViewService
    {
        private const string DefaultUserViewTarget = "";
        private const string FormUserViewTarget = "form";

        private readonly IUserService _userDbService;
        private readonly IAccountService _accountDbService;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        private readonly IReservedNameService _reservedNameDbService;
        //private readonly RegisterTasker _registerTasker;
        private readonly IMapper _mapper;

        public UserViewService(IUserService userDbService,
            //IAccountService accountDbService,
            IReservedNameService reservedNameDbService,
            //RegisterTasker registerTasker,
            IMapper mapper)
        {
            _userDbService = userDbService;
           // _accountDbService = accountDbService;
            _reservedNameDbService = reservedNameDbService;
            //_registerTasker = registerTasker;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Create(UserFormViewModel model, string password)
        {
            throw new NotImplementedException();
            //var newModel = _mapper.Map<AppUserFormModel>(model);
            //var resultModel = await _userDbService.Create<AppUserModel>(newModel, password);
            //return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<UserViewModel> Update(UserFormViewModel model)
        {
            throw new NotImplementedException();
            //var newModel = _mapper.Map<AppUserFormModel>(model);
            //var resultModel = await _userDbService.Update<AppUserModel>(newModel);
            //return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<UserViewModel> Delete(long id)
        {
            throw new NotImplementedException();
            //var resultModel = await _userDbService.Delete<AppUserModel>(id);
            //return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<UserViewModel> Restore(long id)
        {
            throw new NotImplementedException();
            //var resultModel = await _userDbService.Restore<AppUserModel>(id);
            //return _mapper.Map<AppUserViewModel>(resultModel);
        }

        //public async Task<AppUserViewModel> Lock(UserLockoutViewModel lockoutModel)
        //{
        //    var end = DateTimeOffset.TryParse(lockoutModel.LockoutEnd, out DateTimeOffset res);

        //    var model = new UserLockoutModel() //TODO сделать маппер с разбором даты
        //    {
        //        Id = lockoutModel.Id,
        //        LockoutEnd = end ? res : null,
        //        Reason = lockoutModel.Reason
        //    };

        //    var resultModel = await _userDbService.Lock<AppUserViewModel>(model);
        //    return _mapper.Map<AppUserViewModel>(resultModel);
        //}

        public async Task<UserViewModel> Unlock(long id)
        {
            var resultModel = await _userDbService.Unlock<UserViewModel>(id);
            return _mapper.Map<UserViewModel>(resultModel);
        }

        public async Task<ListPageViewModel<UserViewModel>> GetAll(UserFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            throw new NotImplementedException();
            //var filterModel = _mapper.Map<AppUserFilterModel>(filter);

            //var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            //var list = await _userDbService.GetAll(new ListQueryModel<AppUserFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            //var vm = ListPageViewModel.Map<AppUserModel, AppUserViewModel>(list, _mapper);

            //return vm;
        }

        public async Task<IUserViewModel> GetOne(long id, string target)
        {
            throw new NotImplementedException();
            //string include = null;
            //// TODO придумать, как разруливать таргеты и типы, в т.ч. на уровне датасервиса

            ////  var model = await _userDbService.GetOne(id);

            //if (target == null) target = string.Empty;

            //var types = new Dictionary<string, Func<long, string, Task<IAppUserViewModel>>>()
            //{
            //    { DefaultUserViewTarget, GetOneForView },
            //    { FormUserViewTarget, GetOneForForm }
            //};

            //return await types[target](id, include);
        }

        private async Task<IUserViewModel> GetOneForForm(long id, string include)
        {
            throw new NotImplementedException();
            //var model = await _userDbService.GetOne<AppUserFormModel>(id, include);
            //return _mapper.Map<AppUserFormViewModel>(model);
        }

        private async Task<IUserViewModel> GetOneForView(long id, string include)
        {
            throw new NotImplementedException();
            //var model = await _userDbService.GetOne<AppUserModel>(id, include);
            //return _mapper.Map<AppUserViewModel>(model);
        }

        public async Task<UserViewModel> GetOneByEmail(string email)
        {
            var model = await _userDbService.GetOneByEmail<UserModel>(email);
            return _mapper.Map<UserViewModel>(model);
        }

        public async Task<Dictionary<string, bool>> CheckExists(string email, string login)
        {
            throw new NotImplementedException();
            //var dict = new Dictionary<string, bool>();

            //if (!string.IsNullOrEmpty(email))
            //    dict.Add(nameof(email), await _userDbService.CheckEmailExists(email));

            //if (!string.IsNullOrEmpty(login))
            //    dict.Add(nameof(login), await _userDbService.CheckUserNameExists(login));

            //return dict;
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

                    //var exists = await _userDbService.CheckUserNameExists(text);
                    //if (exists) result.Messages.Add($"User with login {text} already exists");

                    //result.IsValid = !exists;
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

        //public async Task AddRoles(long id, UserRolesFormViewModel form)
        //{
        //    throw new NotImplementedException();
        //    //var model = _mapper.Map<UserRolesModel>(form);
        //    //model.UserId = id;
        //    //await _userDbService.AddRoles(model);
        //}

        //public async Task RemoveRoles(long id, UserRolesFormViewModel form)
        //{
        //    throw new NotImplementedException();
        //    //var model = _mapper.Map<UserRolesModel>(form);
        //    //model.UserId = id;
        //    //await _userDbService.RemoveRoles(model);
        //}
    }
}

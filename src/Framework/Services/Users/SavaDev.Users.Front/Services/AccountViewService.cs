using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using SavaDev.Base.Front.Services;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.User.Data.Models.Interfaces;
using SavaDev.Base.Users.Data.Models.Interfaces;
using SavaDev.Base.Users.Front.Manager;
using SavaDev.Base.Users.Front.Models;
using SavaDev.Base.Users.Front.Services;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using SavaDev.General.Data.Contract;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Contract.Models;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Contract.Models;
using System.Text;

namespace Framework.DefaultUser.Service.Services
{
    public class AccountViewService : BaseAccountViewService, IAccountViewService
    {
        public AccountViewService(IUserService userDbService,
           IAccountService accountDbService,
           IReservedNameService reservedNameDbService,
           IMailSender mailSender,
           IMapper mapper) : base(userDbService, accountDbService, reservedNameDbService, mailSender, mapper)
        {
        }

        public async Task<OperationViewResult> Register(RegisterViewModel model)
        {
            var result = await Register<UserFormModel>(model);
            return result;
        }

        protected override IUserFormModel MapForm(IRegisterViewModel model)
        {
            return _mapper.Map<UserFormModel>(model);
        }

        protected override async Task<IUserModel> GetUser(string loginOrEmail)
        {
            return await _userDbService.GetOneByLoginOrEmail<UserModel>(loginOrEmail);
        }

        public async Task RequestNewPassword(RequestNewPasswordFormViewModel model)
        {
            throw new NotImplementedException();
            //var user = await _userDbService.GetOneByLoginOrEmail<AppUserModel>(model.LoginOrEmail);

            //// TODO проверку на нулл

            //var token = await _accountDbService.GeneratePasswordResetToken(user.Email);
            //byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            //var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            //var resetPasswordUrl = $"https://localhost:5001/account/resetpassword?email={user.Email}&token={codeEncoded}";

            //// TODO отрефакторить
            //var mailData = new MailDataModel
            //{
            //    Email = user.Email,
            //    Action = "resetpassword",
            //    Culture = "en",
            //    Variables = new List<MailVariableModel>
            //    {
            //        new MailVariableModel{ Name = "<%Email%>", Value = user.Email },
            //        new MailVariableModel{ Name = "<%UserName%>", Value = user.Login },
            //        new MailVariableModel{ Name = "<%ConfirmUrl%>", Value = resetPasswordUrl}
            //    }
            //};

            //var jsonMessage = JsonSerializer.Serialize(mailData);

            //_registerTasker.Send(jsonMessage);
        }

        public async Task ResetPassword(ResetPasswordFormViewModel model)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            await _accountDbService.ResetPassword(model.Email, codeDecoded, model.NewPassword);
        }

        public async Task<SignInResultViewModel> SignIn(LoginViewModel model)
        {
            var mapped = _mapper.Map<LoginModel>(model);
            throw new NotImplementedException();
            //var result = await _accountDbService.SignIn<UserModel>(mapped);
            //return _mapper.Map<SignInResultViewModel>(result);
        }

        public async Task SignOut()
        {
            throw new NotImplementedException();
            //await _signInManagerAdapter.SignOut();
        }
    }
}

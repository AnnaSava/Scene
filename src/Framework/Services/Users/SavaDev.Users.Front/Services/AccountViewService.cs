using AutoMapper;
using Framework.DefaultUser.Service.Contract;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.WebUtilities;
using SavaDev.Base.User.Data.Models;
using SavaDev.Base.User.Data.Services;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.Mail.Service.Contract;
using SavaDev.System.Data.Contract;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Contract.Models;
using System.Text;
using System.Text.Json;

namespace Framework.DefaultUser.Service.Services
{
    public class AccountViewService : IAccountViewService
    {
        private readonly IUserService _userDbService;
        private readonly IAccountService _accountDbService;
        private readonly IReservedNameService _reservedNameDbService;
        //private readonly RegisterTasker _registerTasker;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;

        public AccountViewService(IUserService userDbService,
           IAccountService accountDbService,
           IReservedNameService reservedNameDbService,
           //RegisterTasker registerTasker,
           IMailSender _mailSender,
           IMapper mapper)
        {
            _userDbService = userDbService;
            _accountDbService = accountDbService;
            _reservedNameDbService = reservedNameDbService;
           // _registerTasker = registerTasker;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Register(RegisterViewModel model)
        {
            // TODO переделать на возврат списка ошибок?
            if (await _userDbService.CheckEmailExists(model.Email))
                throw new Exception("Email exists!");

            if (await _userDbService.CheckLoginExists(model.Login)) // TODO: может проверять одним запросом к БД?
                throw new Exception("Username exists!");

            if (await _reservedNameDbService.CheckIsReserved(model.Login))
                throw new Exception("UserName is forbidden!");

            var newModel = _mapper.Map<UserFormModel>(model);
            var result = await _userDbService.Create(newModel, model.Password);
            var resultModel = result.ProcessedObject as UserFormModel;

            if (resultModel == null || resultModel.Id == 0)
                throw new Exception("Registration error");

            //var token = await _accountDbService.GenerateEmailConfirmationToken(resultModel.Email);
            //byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            //var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            //// TODO прокидывать сюда из настроек урл для подтверждения
            //var confirmUrl = $"https://localhost:5001/account/confirmemail?email={resultModel.Email}&code={codeEncoded}";

            //// TODO отправлять письмо с подтверждением
            //// TODO отрефакторить
            //var mailData = new MailDataModel
            //{
            //    Email = resultModel.Email,
            //    Action = "registration",
            //    Culture = "en",
            //    Variables = new List<MailVariableModel>
            //    {
            //        new MailVariableModel{ Name = "<%Email%>", Value=resultModel.Email },
            //        new MailVariableModel{ Name = "<%UserName%>", Value = resultModel.Login },
            //        new MailVariableModel{ Name = "<%ConfirmUrl%>", Value = confirmUrl}
            //    }
            //};

            //var jsonMessage = JsonSerializer.Serialize(mailData);

            ////_registerTasker.Send(jsonMessage);

            return _mapper.Map<UserViewModel>(resultModel);
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            return await _accountDbService.ConfirmEmail(email, codeDecoded);
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

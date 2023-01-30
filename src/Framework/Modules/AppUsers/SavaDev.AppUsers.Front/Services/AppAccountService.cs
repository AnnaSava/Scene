using AutoMapper;
using Framework.Base.Types.ModelTypes;
using Framework.DefaultUser.Data.Contract;
using Framework.DefaultUser.Service.Contract;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.DataService.Services;
using Framework.User.Service.Contract.Models;
using Framework.User.Service.Services;
using Framework.User.Service.Taskers;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Framework.DefaultUser.Service.Services
{
    public class AppAccountService : BaseAccountService, IAppAccountService
    {
        private readonly AppUserDbService _userDbService;
        private readonly IAppAccountDbService _accountDbService;
        private readonly ISignInManagerAdapter _signInManagerAdapter;
        private readonly IReservedNameDbService _reservedNameDbService;
        private readonly RegisterTasker _registerTasker;
        private readonly IMapper _mapper;

        public AppAccountService(IAppUserDbService userDbService,
           IAppAccountDbService accountDbService,
           ISignInManagerAdapter signInManagerAdapter,
           IReservedNameDbService reservedNameDbService,
           RegisterTasker registerTasker,
           IMapper mapper)
        {
            //_userDbService = userDbService;
            _accountDbService = accountDbService;
            _signInManagerAdapter = signInManagerAdapter;
            _reservedNameDbService = reservedNameDbService;
            _registerTasker = registerTasker;
            _mapper = mapper;
        }

        public async Task<AppUserViewModel> Register(AppRegisterViewModel model)
        {
            // TODO переделать на возврат списка ошибок?
            if (await _userDbService.CheckEmailExists(model.Email))
                throw new Exception("Email exists!");

            if (await _userDbService.CheckUserNameExists(model.Login)) // TODO: может проверять одним запросом к БД?
                throw new Exception("Username exists!");

            if (await _reservedNameDbService.CheckIsReserved(model.Login))
                throw new Exception("UserName is forbidden!");

            var newModel = _mapper.Map<AppUserFormModel>(model);
            var resultModel = await _userDbService.Create<AppUserModel>(newModel, model.Password);

            if (resultModel == null || resultModel.Id == 0)
                throw new Exception("Registration error");

            var token = await _accountDbService.GenerateEmailConfirmationToken(resultModel.Email);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            // TODO прокидывать сюда из настроек урл для подтверждения
            var confirmUrl = $"https://localhost:5001/account/confirmemail?email={resultModel.Email}&code={codeEncoded}";

            // TODO отправлять письмо с подтверждением
            // TODO отрефакторить
            var mailData = new MailDataModel
            {
                Email = resultModel.Email,
                Action = "registration",
                Culture = "en",
                Variables = new List<MailVariableModel>
                {
                    new MailVariableModel{ Name = "<%Email%>", Value=resultModel.Email },
                    new MailVariableModel{ Name = "<%UserName%>", Value = resultModel.Login },
                    new MailVariableModel{ Name = "<%ConfirmUrl%>", Value = confirmUrl}
                }
            };

            var jsonMessage = JsonSerializer.Serialize(mailData);

            _registerTasker.Send(jsonMessage);

            return _mapper.Map<AppUserViewModel>(resultModel);
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            return await _accountDbService.ConfirmEmail(email, codeDecoded);
        }

        public async Task RequestNewPassword(AppRequestNewPasswordFormViewModel model)
        {
            var user = await _userDbService.GetOneByLoginOrEmail<AppUserModel>(model.LoginOrEmail);

            // TODO проверку на нулл

            var token = await _accountDbService.GeneratePasswordResetToken(user.Email);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            var resetPasswordUrl = $"https://localhost:5001/account/resetpassword?email={user.Email}&token={codeEncoded}";

            // TODO отрефакторить
            var mailData = new MailDataModel
            {
                Email = user.Email,
                Action = "resetpassword",
                Culture = "en",
                Variables = new List<MailVariableModel>
                {
                    new MailVariableModel{ Name = "<%Email%>", Value = user.Email },
                    new MailVariableModel{ Name = "<%UserName%>", Value = user.Login },
                    new MailVariableModel{ Name = "<%ConfirmUrl%>", Value = resetPasswordUrl}
                }
            };

            var jsonMessage = JsonSerializer.Serialize(mailData);

            _registerTasker.Send(jsonMessage);
        }

        public async Task ResetPassword(AppResetPasswordFormViewModel model)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            await _accountDbService.ResetPassword(model.Email, codeDecoded, model.NewPassword);
        }

        public async Task<AppSignInResultViewModel> SignIn(AppLoginViewModel model)
        {
            var mapped = _mapper.Map<LoginModel>(model);
            var result = await _accountDbService.SignIn<AppUserModel>(mapped);
            return _mapper.Map<AppSignInResultViewModel>(result);
        }

        public async Task SignOut()
        {
            await _signInManagerAdapter.SignOut();
        }
    }
}

using AutoMapper;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Front.Services;
using SavaDev.Base.User.Data.Models.Interfaces;
using SavaDev.Base.Users.Data.Models.Interfaces;
using SavaDev.Base.Users.Data.Services.Interfaces;
using SavaDev.Base.Users.Front.Models;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.Mail.Service.Contract;
using SavaDev.Mail.Service.Contract.Models;
using SavaDev.General.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Front.Services
{
    public abstract class BaseAccountViewService
    {
        protected readonly IBaseUserService _userDbService;
        protected readonly IAccountService _accountDbService;
        protected readonly IReservedNameService _reservedNameDbService;
        protected readonly IMailSender _mailSender;
        protected readonly IMapper _mapper;

        public BaseAccountViewService(IBaseUserService userDbService,
            IAccountService accountDbService,
            IReservedNameService reservedNameDbService,
            IMailSender mailSender,
            IMapper mapper)
        {
            _userDbService = userDbService;
            _accountDbService = accountDbService;
            _reservedNameDbService = reservedNameDbService;
            _mailSender = mailSender;
            _mapper = mapper;
        }

        protected async Task<OperationViewResult> Register<TUserForm>(IRegisterViewModel model) where TUserForm : class, IUserFormModel
        {
            await ValidateRegistration(model);

            var newModel = MapForm(model); // _mapper.Map<TUserForm>(model);

            var result = await _userDbService.Create(newModel, model.Password);
            var resultModel = HandleRegistrationResult(result);

            var mailData = FillRegistrationMailData(resultModel, await FillConfirmUrl(resultModel));

            await _mailSender.SendInfo(mailData);

            return new OperationViewResult(1);
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            return await _accountDbService.ConfirmEmail(email, token);
        }

        public async Task RequestNewPassword(IRequestNewPasswordFormViewModel model)
        {
            var user = await GetUser(model.LoginOrEmail);
            var mailData = FillRequestNewPasswordMailData(user, await FillResetPasswordUrl(user));

            await _mailSender.SendInfo(mailData);
        }

        protected virtual async Task ValidateRegistration(IRegisterViewModel model)
        {
            if (await _userDbService.CheckEmailExists(model.Email))
                throw new Exception("Email exists!");

            if (await _userDbService.CheckLoginExists(model.Login)) // TODO: может проверять одним запросом к БД?
                throw new Exception("Username exists!");

            if (await _reservedNameDbService.CheckIsReserved(model.Login))
                throw new Exception("UserName is forbidden!");
        }

        protected abstract IUserFormModel MapForm(IRegisterViewModel model);

        protected abstract Task<IUserModel> GetUser(string loginOrEmail);
       
        protected virtual MailDataModel FillRegistrationMailData(IUserFormModel resultModel, string confirmUrl)
        {
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

            return mailData;
        }

        protected virtual IUserFormModel HandleRegistrationResult(OperationResult result)
        {
            var resultModel = result.GetProcessedObject<IUserFormModel>();

            if (resultModel == null || resultModel.Id == 0)
                throw new Exception("Registration error");
            return resultModel;
        }

        protected virtual MailDataModel FillRequestNewPasswordMailData(IUserModel user, string resetPasswordUrl)
        {
            //TODO отрефакторить
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

            return mailData;
        }

        private async Task<string> FillConfirmUrl(IUserFormModel resultModel)
        {
            var token = await _accountDbService.GenerateEmailConfirmationToken(resultModel.Email);
            // TODO прокидывать сюда из настроек урл для подтверждения
            var confirmUrl = $"https://localhost:5001/account/confirmemail?email={resultModel.Email}&code={token}";
            return confirmUrl;
        }

        private async Task<string> FillResetPasswordUrl(IUserModel user)
        {
            var token = await _accountDbService.GeneratePasswordResetToken(user.Email);
            var resetPasswordUrl = $"https://localhost:5001/account/resetpassword?email={user.Email}&token={token}";
            return resetPasswordUrl;
        }
    }
}

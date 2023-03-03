using AutoMapper;
using SavaDev.Base.Front.Services;
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
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Front.Manager
{
    public class RegisterViewManager
    {
        private readonly IBaseUserService _userDbService;
        private readonly IAccountService _accountDbService;
        private readonly IReservedNameService _reservedNameDbService;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;

        public RegisterViewManager(IBaseUserService userDbService,
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

        public async Task<OperationViewResult> Register<TUserForm>(IRegisterViewModel model) where TUserForm : class, IUserFormModel
        {
            if (await _userDbService.CheckEmailExists(model.Email))
                throw new Exception("Email exists!");

            if (await _userDbService.CheckLoginExists(model.Login)) // TODO: может проверять одним запросом к БД?
                throw new Exception("Username exists!");

            if (await _reservedNameDbService.CheckIsReserved(model.Login))
                throw new Exception("UserName is forbidden!");

            var newModel = _mapper.Map<TUserForm>(model);
            //newModel.FirstName = ""; // TODO убрать. Либо разрешить нуллы в БД, либо проставлять где-нибудь, например, в маппере
            //newModel.LastName = "";
            //newModel.DisplayName = "";

            var result = await _userDbService.Create(newModel, model.Password);
            var resultModel = result.ProcessedObject as TUserForm;

            if (resultModel == null || resultModel.Id == 0)
                throw new Exception("Registration error");

            var token = await _accountDbService.GenerateEmailConfirmationToken(resultModel.Email);
            // TODO прокидывать сюда из настроек урл для подтверждения
            var confirmUrl = $"https://localhost:5001/account/confirmemail?email={resultModel.Email}&code={token}";

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

            await _mailSender.SendInfo(mailData);

            return new OperationViewResult(1);
        }
    }
}

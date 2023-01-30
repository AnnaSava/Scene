using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DefaultUser.Service.Contract
{
    public interface IAppAccountService
    {
        Task<AppUserViewModel> Register(AppRegisterViewModel model);

        Task<bool> ConfirmEmail(string email, string token);

        Task RequestNewPassword(AppRequestNewPasswordFormViewModel model);

        Task ResetPassword(AppResetPasswordFormViewModel model);

        Task<AppSignInResultViewModel> SignIn(AppLoginViewModel model);

        Task SignOut();
    }
}

using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DefaultUser.Service.Contract
{
    public interface IAccountViewService
    {
        Task<UserViewModel> Register(RegisterViewModel model);

        Task<bool> ConfirmEmail(string email, string token);

        Task RequestNewPassword(RequestNewPasswordFormViewModel model);

        Task ResetPassword(ResetPasswordFormViewModel model);

        Task<SignInResultViewModel> SignIn(LoginViewModel model);

        Task SignOut();
    }
}

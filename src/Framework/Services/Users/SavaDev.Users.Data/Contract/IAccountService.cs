using SavaDev.Base.User.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DefaultUser.Data.Contract
{
    [Obsolete]
    public interface IAccountService
    {
        Task<string> GenerateEmailConfirmationToken(string email);

        Task<bool> ConfirmEmail(string email, string token);

        Task<string> GeneratePasswordResetToken(string email);

        Task ResetPassword(string email, string token, string newPassword);

        Task<SignInResultModel<TUserOutModel>> SignIn<TUserOutModel>(LoginModel model) where TUserOutModel : BaseUserModel;
    }
}

using Microsoft.AspNetCore.Identity;
using SavaDev.Base.User.Data.Models;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Security.Contract
{
    public interface IAccountService
    {
        Task<string> GenerateEmailConfirmationToken(string email);

        Task<bool> ConfirmEmail(string email, string token);

        Task<string> GeneratePasswordResetToken(string email);

        Task ResetPassword(string email, string token, string newPassword);

        Task<SignInResultModel<TUserOutModel>> SignIn<TUserOutModel>(LoginModel model) where TUserOutModel : BaseUserModel;

        Task SignOut();

        Task<SignInResult> CheckPasswordSignIn(string email, string password);
    }
}

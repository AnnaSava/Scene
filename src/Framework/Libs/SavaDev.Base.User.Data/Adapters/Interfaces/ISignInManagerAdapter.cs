using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SavaDev.Base.User.Data.Adapters.Interfaces
{
    public interface ISignInManagerAdapter
    {
        Task<SignInResult> SignIn(string identifier, string password, bool rememberMe);

        Task SignOut();

        Task<SignInResult> CheckPasswordSignIn(string email, string password);
    }
}

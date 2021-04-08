using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IUserDbService<TUserModel>
    {
        Task<TUserModel> CreateAsync(TUserModel model, string password);

        Task SignIn(TUserModel user, bool rememberMe);

        Task<SignInResult> SignIn(string email, string password, bool rememberMe);

        Task SignOut();

        Task<TUserModel> GetOneByEmail(string email);

        Task<SignInResult> CheckPasswordSignIn(TUserModel model, string password);
    }
}

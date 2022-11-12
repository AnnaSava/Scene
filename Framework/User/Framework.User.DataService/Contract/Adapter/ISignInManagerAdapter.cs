using Framework.User.DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface ISignInManagerAdapter
    {
        Task<SignInResult> SignIn(string identifier, string password, bool rememberMe);

        Task SignOut();

        Task<SignInResult> CheckPasswordSignIn(string email, string password);
    }
}

﻿using SavaDev.Base.Front.Services;
using SavaDev.Users.Front.Contract.Models;

namespace SavaDev.Users.Front.Contract
{
    public interface IAccountViewService
    {
        Task<OperationViewResult> Register(RegisterViewModel model);

        Task<bool> ConfirmEmail(string email, string token);

        Task RequestNewPassword(RequestNewPasswordFormViewModel model);

        Task ResetPassword(ResetPasswordFormViewModel model);

        Task<SignInResultViewModel> SignIn(LoginViewModel model);

        Task SignOut();
    }
}

using Framework.DefaultUser.Service.Contract;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAppAccountService _accountService;

        public ResetPasswordModel(IAppAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public AppResetPasswordFormViewModel Input { get; set; } = new AppResetPasswordFormViewModel();

        public void OnGet(string email, string token)
        {
            Input.Email = email;
            Input.Token = token;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _accountService.ResetPassword(Input);
            return RedirectToPage("/Account/Login");
        }
    }
}

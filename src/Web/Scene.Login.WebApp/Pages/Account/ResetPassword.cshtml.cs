using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Contract.Models;

namespace Scene.Login.WebApp.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAccountViewService _accountService;

        public ResetPasswordModel(IAccountViewService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public ResetPasswordFormViewModel Input { get; set; } = new ResetPasswordFormViewModel();

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

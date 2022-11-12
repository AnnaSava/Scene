using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IFrameworkUserService _userService;

        public ResetPasswordModel(IFrameworkUserService userService)
        {
            _userService = userService;
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
            await _userService.ResetPassword(Input);
            return RedirectToPage("/Account/Login");
        }
    }
}

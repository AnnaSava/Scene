using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Contract.Models;

namespace Scene.Login.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        IAccountViewService _accountService;

        public LoginModel(IAccountViewService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = "")
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.SignIn(Input);
                if (result.Succeeded)
                    return string.IsNullOrEmpty(ReturnUrl) ? Redirect("~/") : Redirect(ReturnUrl);

                ModelState.AddModelError("", "Ошибка входа: неверный логин или пароль");
                
            }

            return Page();
        }
    }
}

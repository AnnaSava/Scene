using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        IFrameworkUserService _userService;

        public LoginModel(IFrameworkUserService userService)
        {
            _userService = userService;
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
                var result = await _userService.SignIn(Input);
                if (result.Succeeded)
                    return string.IsNullOrEmpty(ReturnUrl) ? Redirect("~/") : Redirect(ReturnUrl);

                ModelState.AddModelError("", "Ошибка входа: неверный логин или пароль");
                
            }

            return Page();
        }
    }
}

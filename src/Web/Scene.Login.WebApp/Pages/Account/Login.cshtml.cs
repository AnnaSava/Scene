using Framework.DefaultUser.Service.Contract;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

                ModelState.AddModelError("", "������ �����: �������� ����� ��� ������");
                
            }

            return Page();
        }
    }
}

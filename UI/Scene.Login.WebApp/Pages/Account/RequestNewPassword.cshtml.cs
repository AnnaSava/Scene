using Framework.DefaultUser.Service.Contract;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class RequestNewPasswordModel : PageModel
    {
        private readonly IAppAccountService _accountService;

        public RequestNewPasswordModel(IAppAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public RequestNewPasswordFormViewModel Input { get; set; }

        public bool IsSent { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _accountService.RequestNewPassword(Input);
            IsSent = true;
            return Page();
        }
    }
}

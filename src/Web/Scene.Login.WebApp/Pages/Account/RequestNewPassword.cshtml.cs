using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Contract.Models;

namespace Scene.Login.WebApp.Pages.Account
{
    public class RequestNewPasswordModel : PageModel
    {
        private readonly IAccountViewService _accountService;

        public RequestNewPasswordModel(IAccountViewService accountService)
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

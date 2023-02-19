using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.Users.Front.Contract;

namespace Scene.Login.WebApp.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        IAccountViewService _accountService;

        public ConfirmEmailModel(IAccountViewService accountService)
        {
            _accountService = accountService;
        }

        public bool IsConfirmed { get; set; }

        public async Task OnGetAsync(string email, string code)
        {
            IsConfirmed = await _accountService.ConfirmEmail(email, code);
        }
    }
}

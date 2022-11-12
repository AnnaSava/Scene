using Framework.DefaultUser.Service.Contract;
using Framework.User.Service.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        IAppAccountService _accountService;

        public ConfirmEmailModel(IAppAccountService accountService)
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

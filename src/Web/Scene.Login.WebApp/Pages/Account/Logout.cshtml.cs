using Framework.DefaultUser.Service.Contract;
using Framework.User.Service.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    [IgnoreAntiforgeryToken]
    public class LogoutModel : PageModel
    {
        private readonly IAppAccountService _accountService;

        public LogoutModel(IAppAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // удаляем аутентификационные куки
            await _accountService.SignOut();
            return Redirect("~/");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.Users.Front.Contract;

namespace Scene.Login.WebApp.Pages.Account
{
    [IgnoreAntiforgeryToken]
    public class LogoutModel : PageModel
    {
        private readonly IAccountViewService _accountService;

        public LogoutModel(IAccountViewService accountService)
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

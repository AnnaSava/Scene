using Framework.User.Service.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    [IgnoreAntiforgeryToken]
    public class LogoutModel : PageModel
    {
        private readonly IFrameworkUserService _userService;

        public LogoutModel(IFrameworkUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ������� ������������������ ����
            await _userService.SignOut();
            return Redirect("~/");
        }
    }
}

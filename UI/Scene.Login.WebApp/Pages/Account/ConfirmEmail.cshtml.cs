using Framework.User.Service.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        IFrameworkUserService _userService;

        public ConfirmEmailModel(IFrameworkUserService userService)
        {
            _userService = userService;
        }

        public bool IsConfirmed { get; set; }

        public async Task OnGetAsync(string email, string code)
        {
            IsConfirmed = await _userService.ConfirmEmail(email, code);
        }
    }
}

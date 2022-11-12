using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    public class RequestNewPasswordModel : PageModel
    {
        private readonly IFrameworkUserService _userService;

        public RequestNewPasswordModel(IFrameworkUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RequestNewPasswordFormViewModel Input { get; set; }

        public bool IsSent { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.RequestNewPassword(Input);
            IsSent = true;
            return Page();
        }
    }
}

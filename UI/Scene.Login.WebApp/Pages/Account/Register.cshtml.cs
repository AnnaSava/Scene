using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        IFrameworkUserService _userService;
        ILegalDocumentService _legalDocumentService;

        public RegisterModel(IFrameworkUserService userService, ILegalDocumentService legalDocumentService)
        {
            _userService = userService;
            _legalDocumentService = legalDocumentService;
            Input = new FrameworkRegisterViewModel();
        }

        [BindProperty]
        public FrameworkRegisterViewModel Input { get; set; }

        public LegalDocumentViewModel Consent { get; set; }

        public string ReturnUrl { get; set; }

        public async Task OnGetAsync(string returnUrl = "")
        {
            Consent = await _legalDocumentService.GetActual<LegalDocumentViewModel>("terms", "en");
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.Register(Input);
                    await _userService.SignIn(new LoginViewModel { Identifier = Input.Login, Password = Input.Password, RememberMe = false });
                    return string.IsNullOrEmpty(ReturnUrl) ? Redirect("~/") : Redirect(ReturnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return Page();
        }
    }
}

using Framework.DefaultUser.Service.Contract;
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
        IAppAccountService _accountService;
        ILegalDocumentService _legalDocumentService;

        public RegisterModel(IAppAccountService accountService, ILegalDocumentService legalDocumentService)
        {
            _accountService = accountService;
            _legalDocumentService = legalDocumentService;
            Input = new AppRegisterViewModel();
        }

        [BindProperty]
        public AppRegisterViewModel Input { get; set; }

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
                //try
                {
                    await _accountService.Register(Input);
                    await _accountService.SignIn(new AppLoginViewModel { Identifier = Input.Login, Password = Input.Password, RememberMe = false });
                    return string.IsNullOrEmpty(ReturnUrl) ? Redirect("~/") : Redirect(ReturnUrl);
                }
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError(string.Empty, ex.Message);
                //}
            }
            return Page();
        }
    }
}

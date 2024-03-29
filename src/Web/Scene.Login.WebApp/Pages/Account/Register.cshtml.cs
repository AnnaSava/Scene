using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.General.Front.Contract;
using SavaDev.General.Front.Contract.Models;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Contract.Models;

namespace Scene.Login.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        IAccountViewService _accountService;
        ILegalDocumentViewService _legalDocumentService;

        public RegisterModel(IAccountViewService accountService, ILegalDocumentViewService legalDocumentService)
        {
            _accountService = accountService;
            _legalDocumentService = legalDocumentService;
            Input = new RegisterViewModel();
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

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
                    await _accountService.SignIn(new LoginViewModel { Identifier = Input.Login, Password = Input.Password, RememberMe = false });
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

using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scene.Login.WebApp.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILegalDocumentService _legalDocumentService;
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILegalDocumentService legalDocumentService,
            ILogger<PrivacyModel> logger)
        {
            _legalDocumentService = legalDocumentService;
            _logger = logger;
        }

        public LegalDocumentViewModel Document { get; set; }

        public async Task OnGetAsync()
        {
            Document = await _legalDocumentService.GetActual<LegalDocumentViewModel>("privacy", "en");
        }
    }
}
using Microsoft.AspNetCore.Mvc.RazorPages;
using SavaDev.System.Front.Contract;
using SavaDev.System.Front.Contract.Models;

namespace Scene.Login.WebApp.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILegalDocumentViewService _legalDocumentService;
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILegalDocumentViewService legalDocumentService,
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
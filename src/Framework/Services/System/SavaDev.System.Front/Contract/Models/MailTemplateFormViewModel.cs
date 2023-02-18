using System.ComponentModel.DataAnnotations;

namespace SavaDev.System.Front.Contract.Models
{
    public class MailTemplateFormViewModel
    {
        [Required]
        public string PermName { get; set; }

        [Required]
        public string Culture { get; set; }

        [Required]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}

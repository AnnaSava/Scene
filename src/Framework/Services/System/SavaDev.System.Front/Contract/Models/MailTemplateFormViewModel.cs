using Framework.Base.Types.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

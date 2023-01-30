using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Manage.DataService.Entities
{
    public class ForbiddenName
    {
        [Key]
        public string Text { get; set; }

        public bool IncludePlural { get; set; }
    }
}

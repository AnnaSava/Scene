using Framework.User.Service.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class AppUserFormViewModel : IAppUserViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public DateTime? Birthday { get; set; }

        public string AvatarPath { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SavaDev.Users.Front.Contract.Models
{
    public class UserFormViewModel : IUserViewModel
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

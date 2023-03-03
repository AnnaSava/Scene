using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.Users.Front.Contract.Models
{
    public class UserFilterViewModel : BaseFilter
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool? PhoneNumberConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public bool? IsBanned { get; set; }
    }
}

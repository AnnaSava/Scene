﻿namespace SavaDev.Users.Front.Contract.Models
{
    public class UserViewModel : IUserViewModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsBanned { get; set; }

        public DateTime? BanEnd { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime RegDate { get; set; }

        public string AvatarPath { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.User.DataService.Contract.Models
{
    public class AuthTokenModel
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string AuthJti { get; set; }

        public string RefreshJti { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}

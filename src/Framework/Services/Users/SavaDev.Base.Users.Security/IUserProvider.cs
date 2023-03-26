using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Security
{
    public interface IUserProvider
    {
        string? UserId { get; set; }

        long LongUserId
        {
            get
            {
                if (long.TryParse(UserId, out long id))
                    return id;
                return 0;

            }
        }
    }
}

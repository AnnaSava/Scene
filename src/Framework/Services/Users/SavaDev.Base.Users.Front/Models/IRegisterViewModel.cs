using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SavaDev.Base.Users.Front.Models
{
    public interface IRegisterViewModel
    {
        string Login { get; set; }

        string Email { get; set; }

        string Password { get; set; }
    }
}

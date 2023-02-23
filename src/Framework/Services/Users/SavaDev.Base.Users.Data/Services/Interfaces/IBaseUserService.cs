using SavaDev.Base.Data.Services;
using SavaDev.Base.Users.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Users.Data.Services.Interfaces
{
    public interface IBaseUserService
    {
        Task<OperationResult> Create(IUserFormModel model, string password);

        Task<bool> CheckEmailExists(string email);

        Task<bool> CheckLoginExists(string userName);
    }
}

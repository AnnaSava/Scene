using Framework.Manage.DataService.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Manage.DataService.Contract.Interfaces
{
    public interface IForbiddenNameDbService
    {
        Task<ForbiddenNameModel> Create(ForbiddenNameModel model);

        Task<ForbiddenNameModel> Update(ForbiddenNameModel model);

        Task<string> Remove(string text);

        Task<IEnumerable<ForbiddenNameModel>> GetAll();

        Task<bool> CheckExists(string text);
    }
}

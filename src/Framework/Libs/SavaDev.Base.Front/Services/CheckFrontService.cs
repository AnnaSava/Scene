using SavaDev.Base.Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public static class CheckFrontService
    {
        public static ServiceCheckOk Check(ServiceCheckQuery query)
        {
            var ok = new ServiceCheckOk();
            if (!string.IsNullOrEmpty(query.Message))
            {
                ok.Message = $"{ok.Message} {query.Message}";
            }
            return ok;
        }
    }
}

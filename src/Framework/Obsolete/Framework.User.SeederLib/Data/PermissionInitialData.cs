using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.SeederLib.Data
{
    public class PermissionInitialData
    {
        public static Dictionary<string, string> PermissionsRuCulture => new Dictionary<string, string>
        {
            { "user.create", "Создание нового пользователя" },
            { "user.update", "Редактирование пользователя" },
        };
    }
}

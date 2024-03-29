﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Data.Seeder.Data
{
    public class MailTemplateData
    {
        public static Dictionary<string, string> MailTemplateRuCulture => new Dictionary<string, string>
        {
            { "registration", "Вы зарегистрированы" },
            { "confirmation", "Почтовый адрес подтвержден" },
        };

        public static Dictionary<string, string> MailTemplateEnCulture => new Dictionary<string, string>
        {
            { "registration", "You are registered" },
            { "confirmation", "Email address confirmed" },
        };
    }
}

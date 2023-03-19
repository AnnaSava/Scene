using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Seeder
{
    public interface ISeeder
    {
        Task Seed();
    }
}

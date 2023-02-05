using SavaDev.Base.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Seeder
{
    public class TableSeeder : ISeeder
    {
        private readonly SchemeContext context;

        public TableSeeder(SchemeContext dbContext)
        {
            context = dbContext;
        }

        public async Task Seed()
        {

        }
    }
}

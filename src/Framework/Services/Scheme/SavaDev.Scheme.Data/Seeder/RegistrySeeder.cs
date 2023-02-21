using SavaDev.Base.Data.Context;
using SavaDev.Infrastructure.Util.SchemeManager;
using SavaDev.Scheme.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Data.Seeder
{
    public class RegistrySeeder : ISeeder
    {
        private readonly SchemeContext context;
        private readonly Dictionary<string, string> modulesAndNamespaces;

        public RegistrySeeder(SchemeContext dbContext, Dictionary<string, string> modulesAndNamespaces)
        {
            context = dbContext;
            this.modulesAndNamespaces = modulesAndNamespaces;
        }

        public async Task Seed()
        {
            var manager = new ModelFieldsObjectManager();

            var objectTables = new List<Registry>();

            foreach (var module in modulesAndNamespaces)
            {
                var tablesWithColumns = manager.GetModelAndProps(module.Value);

                foreach(var table in tablesWithColumns)
                {
                    var objectTable = new Registry
                    {
                        Entity = table.Key,
                        Module = module.Key,
                        Columns = new List<Column>()
                    };

                    foreach(var prop in table.Value)
                    {
                        objectTable.Columns.Add(new Column { 
                            Name = prop.Key,
                            DataType = prop.Value,
                            TableId = objectTable.Id,
                            TableName = objectTable.Entity
                        });
                    }
                    objectTables.Add(objectTable);
                }
            }

            try
            {
                context.Registries.AddRange(objectTables);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}

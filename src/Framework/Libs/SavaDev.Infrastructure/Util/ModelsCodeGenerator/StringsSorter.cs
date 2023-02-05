using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelsCodeGenerator
{
    public class StringsSorter
    {
        public void SortImportsRazor()
        {
            var files = new List<string>
            { 
                "D:\\Rep\\Sava\\src\\Web\\Sava.Manage.BlazorServer.\\_Imports.razor",            
                "D:\\Rep\\Sava\\src\\Web\\Scene.Manage.MudBlazorServer.\\_Imports.razor",
                "D:\\Rep\\Sava\\src\\Web\\Sava.Planner.BlazorServer.\\_Imports.razor"
            };

            foreach (var file in files)
            {
                var strings = new List<string>();
                using (var reader = new StreamReader(file))
                {
                    while(reader.Peek() >=0)
                    {
                        strings.Add(reader.ReadLine());
                    }
                }

                strings = strings.Distinct().OrderBy(x => x).ToList();

                using (var writer = new StreamWriter(file))
                {
                    foreach (var s in strings)
                    {
                        writer.WriteLine(s);
                    }
                }
            }
        }
    }
}

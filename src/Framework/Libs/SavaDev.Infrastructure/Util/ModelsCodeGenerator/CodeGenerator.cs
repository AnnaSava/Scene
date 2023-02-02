using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ModelsCodeGenerator
{
    public class CodeGenerator
    {
        public void GetFiles(string path)
        {
            var dirs = Directory.GetDirectories(path, "*Entities*", SearchOption.AllDirectories);
            var output = "Entity";

            foreach (var dir in dirs.Take(10))
            {
                if (dir.Contains("Obsolete") || dir.Contains(".hg") || dir.Contains(".Base."))
                    continue;

                var files = Directory.GetFiles(dir, "*.cs");

                foreach (var file in files)
                {
                    var shortName = Path.GetFileNameWithoutExtension(file);

                    if (shortName.StartsWith("Base"))
                        continue;

                    var newDir = new DirectoryInfo(dir).Parent.Name;
                    var newPath = Path.Combine(output, "Models", newDir);
                    var newFilePath = Path.Combine(newPath, shortName + "Model.cs");

                    var strings = new List<string>();

                    using (var sr = new StreamReader(file))
                    {
                        while (sr.Peek() >= 0)
                        {
                            strings.Add(sr.ReadLine());
                        }
                    }

                    var sb = new StringBuilder();

                    foreach (var line in strings)
                    {
                        if (line.Contains("namespace"))
                            continue;

                        string modified = line;

                        if (line.Contains(shortName))
                            modified = line.Replace(shortName, shortName + "Model");

                        var virt = "virtual";

                        if (line.Contains(virt))
                        {
                            // TODO переписать на регулярку?
                            modified = line.Replace("{ get; set; }", "").TrimEnd();
                            var lastSpaceIndex = modified.LastIndexOf(' ');
                            var start = line.Substring(0, lastSpaceIndex);
                            var end = line.Substring(lastSpaceIndex, line.Length - lastSpaceIndex).Trim();
                            modified = $"{start}Model {end}";
                            modified = modified.Replace("virtual ", "");
                        }

                        sb.AppendLine(modified);
                    }

                    if(!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    using (var sw = new StreamWriter(newFilePath))
                    {
                        sw.Write(sb.ToString());
                    }
                }
            }
        }
    }
}

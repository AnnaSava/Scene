using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.Files
{
    public class MimeTypeModel
    {
        public string Name { get; }

        public byte?[] Bytes { get; }

        public MimeTypeModel(string name, byte?[] bytes)
        {
            Name = name;
            Bytes = bytes;
        }
    }
}

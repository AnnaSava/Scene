using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers.Files
{
    public class MimeTypeChecker
    {
        private readonly byte maxCheckingBytes = 128;
        public string GetMimeType(byte[] file)
        {
            foreach (var mimeType in MimeTypes.Collection)
            {
                for (byte i = 0; i < maxCheckingBytes; i++)
                {
                    if (i < mimeType.Bytes.Length)
                    {
                        if (mimeType.Bytes[i] == null)
                            continue;

                        if (file[i] != mimeType.Bytes[i])
                            break;

                        if (i == mimeType.Bytes.Length - 1)
                            return mimeType.Name;
                    }
                }
            }
            return "undefined";
        }

        public string GetExtention(string mimeType)
        {
            if (string.IsNullOrEmpty(mimeType) || mimeType == "undefined") return null;

            var extFromMime = mimeType.Split(',');
            if (extFromMime.Length == 0) return null;

            return extFromMime[0];
        }
    }
}

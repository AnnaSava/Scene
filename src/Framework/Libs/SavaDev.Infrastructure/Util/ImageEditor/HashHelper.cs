using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Infrastructure.Util.ImageEditor
{
    public class HashHelper
    {
        public string GetMd5Hash(byte[] content)
        {
            if (content == null)
                throw new Exception("FileUploader.GetMd5Hash: File content cannot be null!");

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(content);

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        public string GetSha1Hash(byte[] content)
        {
            if (content == null)
                throw new Exception("FileUploader.GetSha1Hash: File content cannot be null!");

            using (HashAlgorithm sha1Hash = SHA1.Create())
            {
                byte[] data = sha1Hash.ComputeHash(content);

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder(data.Length * 2);

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}

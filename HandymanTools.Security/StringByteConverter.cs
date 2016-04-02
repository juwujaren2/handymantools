using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Security
{
    public class StringByteConverter   
    {
        public byte[] ToBytes(string str)
        {
            //var bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            var bytes = Encoding.UTF8.GetBytes(str);
            return bytes;
        }

        public string StringFromBytes(byte[] bytes)
        {
            //var chars = new char[bytes.Length / sizeof(char)];
            //System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            StringBuilder chars = new StringBuilder();
            foreach(byte b in bytes)
            {
                chars.Append(b.ToString("x2"));
            }
            return chars.ToString();
        }
    }
}

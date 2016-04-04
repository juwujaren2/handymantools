using System.Text;

namespace HandymanTools.Security
{
    public class StringByteConverter   
    {
        public byte[] ToBytes(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            return bytes;
        }

        public string BytesToHex(byte[] bytes)
        {
            StringBuilder chars = new StringBuilder();
            foreach(byte b in bytes)
            {
                chars.Append(b.ToString("x2"));
            }
            return chars.ToString();
        }

        public string StringFromBytes(byte[] bytes)
        {
            var result = Encoding.UTF8.GetString(bytes);
            return result;
        }
    }
}

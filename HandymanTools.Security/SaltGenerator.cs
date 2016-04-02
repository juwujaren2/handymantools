using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Security
{
    public class SaltGenerator
    {
        private RNGCryptoServiceProvider m_cryptoServiceProvider;
        private const int SALT_SIZE = 24;

        public SaltGenerator()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public string Salt
        {
            get
            {
                var converter = new StringByteConverter();
                byte[] genSalt = new byte[SALT_SIZE];
                m_cryptoServiceProvider.GetNonZeroBytes(genSalt);
                return converter.StringFromBytes(genSalt);
            }
        }
    }
}

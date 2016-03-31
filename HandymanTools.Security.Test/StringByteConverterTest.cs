using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandymanTools.Security.Test
{
    using HandymanTools.Security;

    [TestClass]
    public class StringByteConverterTest
    {
        private StringByteConverter m_stringByteConverter;

        [TestInitialize]
        public void SetUp()
        {
            m_stringByteConverter = new StringByteConverter();
        }

        [TestMethod]
        public void StringToBytesAndBackShouldWork()
        {
            string orig = "12345";
            byte[] bytes = m_stringByteConverter.ToBytes(orig);
            string result = m_stringByteConverter.StringFromBytes(bytes);
            Assert.AreEqual(orig, result);
        }        
    }
}

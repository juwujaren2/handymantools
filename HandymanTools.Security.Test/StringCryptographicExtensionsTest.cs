using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandymanTools.Security.Test
{
    using HandymanTools.Security;

    [TestClass]
    public class StringCryptographicExtensionsTest
    {
        [TestMethod]
        public void differentStringsShouldHaveDifferentHashesWithSameSalt()
        {
            var str1 = "test1";
            var str2 = "test2";
            var salt = "somesalt";
            Assert.AreNotEqual(str1.ToSHA256(salt), str2.ToSHA256(salt));
        }

        [TestMethod]
        public void sameStringShouldProduceSameHashWithSameSalt()
        {
            var str1 = "test";
            var salt = "somesalt";
            Assert.AreEqual(str1.ToSHA256(salt), str1.ToSHA256(salt));
        }

        [TestMethod]
        public void sameStringShouldProduceDifferentHashesWithDifferentSalts()
        {
            var testStr = "test";
            var salt1 = "salt1";
            var salt2 = "salt2";
            Assert.AreNotEqual(testStr.ToSHA256(salt1), testStr.ToSHA256(salt2));
        }
    }
}

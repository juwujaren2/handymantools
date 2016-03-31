using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandymanTools.Security.Test
{
    [TestClass]
    public class SaltGeneratorTest
    {
        private SaltGenerator m_saltGenerator;

        [TestInitialize]
        public void SetUp()
        {
            m_saltGenerator = new SaltGenerator();
        }

        [TestMethod]
        public void TwoSaltCallsShouldGenerateTwoSaltStrings()
        {
            Assert.AreNotEqual(m_saltGenerator.Salt, m_saltGenerator.Salt);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester
{
    [TestClass]
    public class Intro
    {
        [TestMethod]
        public void HellowWorld()
        {
            int a = 0;
            int b = 1;
            Assert.AreEqual(a, b, "A is not b");
            Assert.AreNotEqual(a, b, "A is not b");
        }
    }
}

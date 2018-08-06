using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LambdaDicom.AttributeModule.find(new LambdaDicom.Tag(0x0001, 0x0002), null);
        }
    }
}

using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddNodeUnitTest
{
    [TestClass]
    public class AddNodeUnitTest
    {
        [TestMethod]
        public void CheckSumofNumbers()
        {
            string[] numbers = new string[7]{"4", "8", "D", "G","12", "4","D"};
            StringBuilder logs = new StringBuilder();
            AddNodeProject.Program.SumNumbers(numbers, logs);

            Assert.IsTrue(logs.Length > 0);
        }
    }
}

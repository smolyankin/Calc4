using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calc4.Tests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        [DataRow("1+2", "3")]
        [DataRow("(12-4)/2", "4")]
        [DataRow("2*(1+3)-7", "1")]
        [DataRow("1.5*2+6/2", "6")]
        [DataRow("1.5*(2+6)/2", "6")]
        [DataRow("(1,2+2,8)*(0,1*100)", "40")]
        [DataRow("(2*(2+5)-3*(1+3))/2", "1")]
        public void Calculate(string input, string expected)
        {
            var actual = Calculator.Execute(input).ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}

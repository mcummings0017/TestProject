using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTestProject.Classes;
using NUnit.Framework;
using UIAutomationClient;

namespace MyTestProject.Tests
{
    [TestFixture]
    class ConversionTests
    {
        [Test]
        public void Test1()
        {
            var d = new Digit(7);

            byte number = d;
            Console.WriteLine(number);  // output: 7

            Digit digit = (Digit)number;
            Console.WriteLine(digit);  // output: 7
        }
    }
}

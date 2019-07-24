using MyTestProject.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationClient;

namespace MyTestProject.Tests
{
    [TestFixture]
    class UIA_Tests
    {
        [Test]
        public void Test1()
        {
            IUIAutomation a = new CUIAutomation();

            IUIAutomationElement x = a.GetRootElement();

            IUIAutomation2 b = new CUIAutomation8();
        }
    }
}

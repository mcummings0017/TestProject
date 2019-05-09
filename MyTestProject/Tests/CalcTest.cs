using NUnit.Framework;
using System.Diagnostics;
using UIAutomationClient;

namespace MyTestProject.Tests
{
    [TestFixture]
    class CalcTest
    {
        [Test]
        public void Test1()
        {
            ProcessStartInfo x = new ProcessStartInfo();
            x.FileName = @"C:\Windows\System32\calc.exe";
            Process.Start(x);

            var ROOT = new CUIAutomationClass().GetRootElement();

            var prc = ROOT.FindAll(TreeScope.TreeScope_Children, new CUIAutomationClass().CreateTrueCondition());

            int size = prc.Length;
            for (int i = 0; i < prc.Length; i++)
            {
                string l = prc.GetElement(i).CurrentName;
                int r = prc.GetElement(i).CurrentProcessId;
            }


            IUIAutomationCondition window_cond = new CUIAutomationClass().CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Calculator");
            var window = ROOT.FindFirst(TreeScope.TreeScope_Children, window_cond);
            
            IUIAutomationCondition el_cond = new CUIAutomationClass().CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "num9Button");

            IUIAutomationElement el = window.FindFirst(TreeScope.TreeScope_Descendants, el_cond);

            var pat = (IUIAutomationInvokePattern)el.GetCurrentPattern(UIA_PatternIds.UIA_InvokePatternId);
            pat.Invoke();
            
            IUIAutomation m = new CUIAutomation();

            int p = 0;
        }

    }
}

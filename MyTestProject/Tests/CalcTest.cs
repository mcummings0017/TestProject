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
            var process = Process.Start(x);
            process.WaitForInputIdle();

            var ROOT = new CUIAutomationClass().GetRootElement();

            var prc = ROOT.FindAll(TreeScope.TreeScope_Children, new CUIAutomationClass().CreateTrueCondition());

            int size = prc.Length;
            for (int i = 0; i < prc.Length; i++)
            {
                string l = prc.GetElement(i).CurrentName;
                int r = prc.GetElement(i).CurrentProcessId;
            }

            ////This is how you attach to an already opened process
            //foreach (Process procs in Process.GetProcesses())
            //{
            //    if (procs.ProcessName.Equals("Calculator"))
            //    {
            //        int ProcessID = procs.Id;
            //    }
            //}


            IUIAutomationCondition window_cond = new CUIAutomationClass().CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "Calculator");

            IUIAutomationElement window = null;
            int attempts = 1;

            while(window == null && attempts < 10)
            {
                window = ROOT.FindFirst(TreeScope.TreeScope_Children, window_cond);
                attempts++;
            }
            
            IUIAutomationCondition el_cond = new CUIAutomationClass().CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "num9Button");

            IUIAutomationElement el = window.FindFirst(TreeScope.TreeScope_Descendants, el_cond);

            var pat = (IUIAutomationInvokePattern)el.GetCurrentPattern(UIA_PatternIds.UIA_InvokePatternId);
            pat.Invoke();
            

            IUIAutomation m = new CUIAutomation();

            IUIAutomationCondition hist_Cond = new CUIAutomationClass().CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "HistoryButton");

            IUIAutomationElement hist_El = window.FindFirst(TreeScope.TreeScope_Descendants, hist_Cond);

            var y = hist_El.CurrentHelpText;

            int p = 0;
        }

    }
}

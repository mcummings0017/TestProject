using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TestStack.White.InputDevices;
using UIAutomationClient;

namespace MyTestProject.Extensions
{
    public enum RelativeType
    {
        Parent,
        FirstChild
    }

    public static class UIAElement_Extensions
    {
        private static IUIAutomationLegacyIAccessiblePattern _LegacyIAccessiblePattern;
        private static IUIAutomationScrollItemPattern _ScrollItemPattern;
        private static IUIAutomationExpandCollapsePattern _ExpandCollapsePattern;
        private static IUIAutomationSelectionPattern _SelectionPattern;
        private static IUIAutomationSelectionItemPattern _SelectionItemPattern;
        private static IUIAutomationTogglePattern _TogglePattern;
        private static IUIAutomationValuePattern _ValuePattern;
        private static IUIAutomationInvokePattern _InvokePattern;

        public static IUIAutomationElement xtGetRelative(this IUIAutomationElement element, RelativeType RelativeType)
        {
            IUIAutomationElement relative = null;

            IUIAutomationTreeWalker walker = new CUIAutomationClass().RawViewWalker;
            switch (RelativeType)
            {
                case RelativeType.Parent:
                    try { relative = walker.GetParentElement(element); }
                    catch (COMException e) { }
                    break;
                case RelativeType.FirstChild:
                    try { relative = walker.GetFirstChildElement(element); }
                    catch (COMException e) { }
                    break;
                default: throw new Exception("Given enum was not present in the switch!!!");
            }

            return relative as IUIAutomationElement;
        }
        public static IUIAutomationElement xtToggleCheckBox(this IUIAutomationElement element, bool Check)
        {
            if (element.CurrentIsEnabled == 1)
            {
                _TogglePattern = (IUIAutomationTogglePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_TogglePatternId);
                bool IsSelected = _TogglePattern.CurrentToggleState == ToggleState.ToggleState_On;
                if (Check != IsSelected)
                {
                    try { _TogglePattern.Toggle(); }
                    catch (COMException e) { }
                }
                Thread.Sleep(10);
            }
            _TogglePattern = (IUIAutomationTogglePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_TogglePatternId);
            if (Check != (_TogglePattern.CurrentToggleState == ToggleState.ToggleState_On))
                throw new Exception("Checkbox was not properly Toggled!!!");
            return element;
        }
        /// <summary>
        /// SelectionItemPattern.Select method. Used for Radio Buttons, Tabs so far.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IUIAutomationElement xtSelectItem(this IUIAutomationElement element)
        {
            _SelectionItemPattern = (IUIAutomationSelectionItemPattern)element.GetCurrentPattern(UIA_PatternIds.UIA_SelectionItemPatternId);
            if (!element.xtIsItemSelected())
                _SelectionItemPattern.Select();
            return element;
        }

        public static IUIAutomationElement xtInvoke(this IUIAutomationElement element)
        {
            _InvokePattern = (IUIAutomationInvokePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_InvokePatternId);
            try { _InvokePattern.Invoke(); } catch (COMException com) { }
            return element;
        }
        public static IUIAutomationElement xtCheckValue(this IUIAutomationElement element, string Value)
        {
            _LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
            if (_LegacyIAccessiblePattern.CurrentValue != Value)
            {
                Console.WriteLine($"Value was not properly set for the Element!!! Element Name: [{element.CurrentName}] - Element ID: [{element.CurrentAutomationId}] - Intended Value: [{Value}] .");
                throw new Exception($"Value was not properly set for the Element!!! Element Name: [{element.CurrentName}] - Element ID: [{element.CurrentAutomationId}] - Intended Value: [{Value}] .");
            }
            return element;
        }
        public static bool xtIsItemSelected(this IUIAutomationElement element)
        {
            _SelectionItemPattern = (IUIAutomationSelectionItemPattern)element.GetCurrentPattern(UIA_PatternIds.UIA_SelectionItemPatternId);
            return _SelectionItemPattern.CurrentIsSelected == 1; // CurrentIsSelected returns int 0 or 1 for binary true/false
        }
        public static ExpandCollapseState xtGetExpandCollapseState(this IUIAutomationElement element)
        {
            _ExpandCollapsePattern = (IUIAutomationExpandCollapsePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_ExpandCollapsePatternId);
            return _ExpandCollapsePattern.CurrentExpandCollapseState;
        }
        public static IUIAutomationElement xtExpand(this IUIAutomationElement element)
        {
            _ExpandCollapsePattern = (IUIAutomationExpandCollapsePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_ExpandCollapsePatternId);
            _ExpandCollapsePattern.Expand();
            return element;
        }
        public static IUIAutomationElement xtCollapse(this IUIAutomationElement element)
        {
            _ExpandCollapsePattern = (IUIAutomationExpandCollapsePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_ExpandCollapsePatternId);
            _ExpandCollapsePattern.Collapse();
            return element;
        }

        public static IUIAutomationElement xtScrollIntoView(this IUIAutomationElement element)
        {
            _ScrollItemPattern = (IUIAutomationScrollItemPattern)element.GetCurrentPattern(UIA_PatternIds.UIA_ScrollItemPatternId);
            _ScrollItemPattern.ScrollIntoView();
            return element;
        }

        public static IUIAutomationElementArray xtGetAllChildren(this IUIAutomationElement element)
        {
            return element.FindAll(TreeScope.TreeScope_Children, new CUIAutomationClass().CreateTrueCondition());
        }

        /// <summary>
        /// DO NOT USE FOR BUTTONS.
        /// </summary>
        /// <param name="element"></param>
        public static void xtDoDefaultAction(this IUIAutomationElement element)
        {
            _LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
            try { _LegacyIAccessiblePattern.DoDefaultAction(); } catch (COMException e) { }
        }

        public static string xtGetValue(this IUIAutomationElement element)
        {
            _LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
            return _LegacyIAccessiblePattern.CurrentValue;
        }
        public static string xtGetDescription(this IUIAutomationElement element)
        {
            _LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
            return _LegacyIAccessiblePattern.CurrentDescription;
        }


        /// <summary>
        /// This method will attempt to set the LegacyIAccessible Value of an element and then press TAB.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static IUIAutomationElement xtSetValue(this IUIAutomationElement element, string value)
        {
            _LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
            _LegacyIAccessiblePattern.SetValue(value);
            Thread.Sleep(100);
            element.SetFocus();
            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
            return element;
        }
        public static IUIAutomationElement xtSetValue2(this IUIAutomationElement element, string value)
        {
            _LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)element.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
            _LegacyIAccessiblePattern.SetValue(value);
            Thread.Sleep(100);
            element.SetFocus();
            Thread.Sleep(100);
            return element;
        }
        /// <summary>
        /// This SetValue method attempts to Click the center of the element and send keys with a Keyboard instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IUIAutomationElement xtSetValue3(this IUIAutomationElement element, string value)
        {
            element.xtSetValue2("");
            Thread.Sleep(100);
            element.xtFocus().xtClickCenterOfBounds();
            Thread.Sleep(100);
            Keyboard.Instance.Enter(value);
            Thread.Sleep(10);
            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
            return element;
        }
        public static IUIAutomationElement xtFocus(this IUIAutomationElement element)
        {
            try
            {
                element.SetFocus();
            }
            catch (Exception e) { }
            Thread.Sleep(100);
            return element;
        }
        public static IUIAutomationElement xtClickCenterOfBounds(this IUIAutomationElement element)
        {
            try { element.SetFocus(); } catch (Exception e) { }
            var rectangle = element.CurrentBoundingRectangle;
            Point center = new Point();
            center.X = rectangle.left + ((rectangle.right - rectangle.left) / 2);
            center.Y = rectangle.top + ((rectangle.bottom - rectangle.top) / 2);
            Mouse.Instance.Location = center;
            Thread.Sleep(250);
            Mouse.Instance.Click(MouseButton.Left, center);
            return element;
        }
        public static void xtSelectFromComboBox(this IUIAutomationElement element, string Option)
        {
            var childrenOfCombobox = element.xtGetAllChildren();
            IUIAutomationElement listItemToSelect = null;

            bool IsInsideTable = element.xtGetRelative(RelativeType.Parent).CurrentLocalizedControlType == "dataitem";

            //	Seeing if the intended Option to Select even exists
            bool found = false;
            for (int i = 0; i < childrenOfCombobox.Length; i++)
            {
                if (childrenOfCombobox.GetElement(i).CurrentName == Option)
                {
                    found = true;
                    listItemToSelect = (IUIAutomationElement)childrenOfCombobox.GetElement(i);
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine($"List Item is not an Option!!! ComboBox Name: [{element.CurrentName}] - ComboBox ID: [{element.CurrentAutomationId}] - List Item Selection: [{Option}] .");
                throw new Exception($"List Item is not an Option!!! ComboBox Name: [{element.CurrentName}] - ComboBox ID: [{element.CurrentAutomationId}] - List Item Selection: [{Option}] .");
            }

            //	If intended list item is already selected - just skip everything - no need
            if (listItemToSelect.xtIsItemSelected())
                return;

            //	Seeing if a blank option exists
            bool blankOptionExists = false;
            for (int i = 0; i < childrenOfCombobox.Length; i++)
            {
                if (childrenOfCombobox.GetElement(i).CurrentName == " ")
                {
                    blankOptionExists = true;
                    break;
                }
            }

            // Block to Complete selection if the combo box is inside a table row
            if (IsInsideTable)
            {
                try { element.xtScrollIntoView(); } catch (Exception e) { }
                Thread.Sleep(50);
                element.xtFocus();
                Thread.Sleep(50);
                element.xtClickCenterOfBounds();

                for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
                {
                    Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.UP);
                }
                for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
                {
                    if (listItemToSelect.xtIsItemSelected())
                        break;
                    Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.DOWN);
                }
                goto czCheckpoint;
            }


            //	Block to complete the ComboBox Selection
            if (blankOptionExists)
            {
                Thread.Sleep(50);
                element.SetFocus();
                Thread.Sleep(50);
                Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SPACE);
                element.xtCollapse();

                for (int i = 0; i < childrenOfCombobox.Length + 2; i++)
                {
                    Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.DOWN);
                    if (listItemToSelect.xtIsItemSelected())
                        break;
                }
            }
            else if (!blankOptionExists)
            {
                Thread.Sleep(50);
                element.SetFocus();
                Thread.Sleep(50);

                element.SetFocus();

                for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
                {
                    Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.UP);
                }
                for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
                {
                    Thread.Sleep(50);
                    if (listItemToSelect.xtIsItemSelected())
                        break;
                    Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.DOWN);
                }
            }


            czCheckpoint:

            if (!listItemToSelect.xtIsItemSelected())
            {
                Console.WriteLine($"ComboBox list item was not properly selected!!! Element Name: [{element.CurrentName}] - Element ID: [{element.CurrentAutomationId}] - List Item Selection: [{Option}] .");
                throw new Exception($"ComboBox list item was not properly selected!!! Element Name: [{element.CurrentName}] - Element ID: [{element.CurrentAutomationId}] - List Item Selection: [{Option}] .");
            }

            Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(100);


        }
    }
}

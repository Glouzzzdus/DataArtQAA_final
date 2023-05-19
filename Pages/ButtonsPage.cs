using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArtQAA_Homework04
{
    public class ButtonsPage:BasePage
    {
        public ButtonsPage(IWebDriver driver):base(driver) { }

        protected override string Url => "https://demoqa.com/buttons";


        //Elements
        private IWebElement _dblClickBtn => driver.FindElement(By.Id("doubleClickBtn"));
        private IWebElement _rightClickBtn => driver.FindElement(By.Id("rightClickBtn"));
        private IWebElement _simpleClickBtn => driver.FindElement(By.XPath("//button[text()='Click Me']"));
        private IWebElement _dblClickMsg => driver.FindElement(By.Id("doubleClickMessage"));
        private IWebElement _rightClickMsg => driver.FindElement(By.Id("rightClickMessage"));
        private IWebElement _simpleClickMsg => driver.FindElement(By.Id("dynamicClickMessage"));

        //Methods
        public void ActivateDoubleclickBtn()
        {
            Actions actions = new(driver);
            actions.DoubleClick(_dblClickBtn).Perform();
        }

        public void ActivateRightClickBtn()
        {
            Actions actions = new(driver);
            actions.ContextClick(_rightClickBtn).Perform();
        }
        public void ActivateSimpleClickBtn()
        {
            _simpleClickBtn.Click();
        }

        public string GetDblClickMsg() 
        { 
            return _dblClickMsg.Text;
        }
        public string GetRightClickMsg()
        {
            return _rightClickMsg.Text;
        }
        public string GetSimpleClickMsg()
        {
            return _simpleClickMsg.Text;
        }

        public bool IsDblClickMsgExist()
        {
            return _dblClickMsg.Displayed;
        }
        public bool IsRightClickMsgExist()
        {
            return _rightClickMsg.Displayed;
        }
        public bool IsSimpleClickMsgExist()
        {
            return _simpleClickMsg.Displayed;
        }

    }
}

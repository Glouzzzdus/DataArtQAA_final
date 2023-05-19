using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArtQAA_Homework04
{
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected abstract string Url { get; }
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Open()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}

using DataArtQAA_Homework04.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArtQAA_Homework04.Framework
{
    public class PageList
    {
        private readonly IWebDriver _driver;

        private ButtonsPage _buttons;
        public ButtonsPage Buttons => _buttons ??= new ButtonsPage(_driver);
        private TablesPage _tables; 
        public TablesPage Tables => _tables ??= new TablesPage(_driver);

        public PageList(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}

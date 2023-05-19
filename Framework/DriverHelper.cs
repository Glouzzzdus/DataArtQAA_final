using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataArtQAA_Homework04.Framework
{
    public class DriverHelper
    {
        public static IWebDriver GetDriver(string browser)
        {
            IWebDriver? driver = null;
            switch (browser)              
            {
                case "chrome":
                    var options = new ChromeOptions();
                    options.AddArgument("--incognito");
                    driver = new ChromeDriver(options);
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentException("Unused browser type");
            }            
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static string MakeScreenshot(IWebDriver driver, string testName)
        {            
            string screenshotFolder = @"d:\reports";
            string screenshotPath = String.Empty;

            if(driver != null)
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var dateTimeString = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                var screenshotName = $"{testName}-{dateTimeString}.png";
                screenshotPath = Path.Combine(screenshotFolder, screenshotName);
                screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            }

            return screenshotPath;
        }
    }
}

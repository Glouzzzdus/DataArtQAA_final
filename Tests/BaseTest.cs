using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataArtQAA_Homework04.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace DataArtQAA_Homework04
{
    public class BaseTest
    {
        protected IWebDriver Driver { get; set; }
        protected ExtentReports extentReports;
        protected ExtentTest extentTest;
        protected PageList Pages { get; private set; }
        //[OneTimeSetUp]
        //protected void SetupReporting() 
        //{
        //    extentReports = new ExtentReports();
        //    string reportPath = @"d:\reports\DA_report.html";
        //    var htmlReporter = new ExtentHtmlReporter(reportPath);
        //    extentReports.AttachReporter(htmlReporter);
        //}
        [SetUp]
        public void Setup()
        {
            Driver = DriverHelper.GetDriver("chrome");
            Pages = new PageList(Driver);
            //extentTest = extentReports.CreateTest(TestContext.CurrentContext.Test.FullName);
        }

        [TearDown]
        public void TearDown()
        {
            //if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            //{
            //    extentTest.Log(Status.Pass);
            //}
            //else
            //{
            //    var screenshotPath = DriverHelper.MakeScreenshot(Driver, testName: TestContext.CurrentContext.Test.MethodName);
            //    extentTest.AddScreenCaptureFromPath(screenshotPath);
            //    extentTest.Log(Status.Fail);
            //}
            Driver.Quit();
        }

        //[OneTimeTearDown]
        //public void ReportFlush()
        //{
        //    extentReports.Flush();
        //}
    }
}

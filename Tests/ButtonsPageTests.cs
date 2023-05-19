using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DataArtQAA_Homework04
{   
    [TestFixture]
    public class ButtonsPageTests:BaseTest
    {
        [Test]
        public void TestButtonsPage()
        {
            Pages.Buttons.Open();

            var expectedDblClickMsg = "You have done a double click";
            var expectedRightClickMsg = "You have done a right click";
            var expectedSimpleClickMsg = "You have done a dynamic click";            
            
            Pages.Buttons.ActivateDoubleclickBtn();
            Pages.Buttons.ActivateRightClickBtn();
            Pages.Buttons.ActivateSimpleClickBtn();

            Assert.True((Pages.Buttons.GetDblClickMsg() == expectedDblClickMsg) && Pages.Buttons.IsDblClickMsgExist());
            Assert.True(Pages.Buttons.IsRightClickMsgExist() && (Pages.Buttons.GetRightClickMsg() == expectedRightClickMsg));
            Assert.True(Pages.Buttons.IsSimpleClickMsgExist() && (Pages.Buttons.GetSimpleClickMsg() == expectedSimpleClickMsg));
        }
    }
}
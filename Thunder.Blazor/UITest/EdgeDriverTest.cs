using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

namespace UITest
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private EdgeDriver driver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            driver = new EdgeDriver(options);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            //_driver.Url = "https://localhost:44317/";
            //Assert.AreEqual("ServerDemo", _driver.Title);
            Assert.IsTrue(true);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            //Task.Delay(10000).Wait();
            driver.Quit();
        }

        [TestMethod]
        public void SelectTest()
        {
            driver.Url = "https://localhost:44317/";
            Task.Delay(500).Wait();
            var select = new SelectElement(driver.FindElement(By.TagName("select")));
            select.SelectByIndex(1);
            //Task.Delay(500).Wait();
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            var v = driver.FindElementById("selectDemo");
            System.Console.WriteLine($"当前选项：{v.Text}");
            Assert.IsTrue(v.Text == "2");
        }
    }
}

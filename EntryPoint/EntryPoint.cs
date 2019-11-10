using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(@"C:\Drivers\chromedriver_win32\");
        }

        [Test]
        public void XPathlementDisplayed_Test()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com/selectors/css-path/");

            IWebElement element;

            try
            {
                element = driver.FindElement(By.XPath("//*[@id=\"post-108\"]/div/figure/img"));

                Thread.Sleep(1000);
                Assert.IsTrue(element.Displayed);
            }
            catch (NoSuchElementException e)
            {
                
                Assert.Fail(e.Message);
            }
            finally
            {
                
                driver.Quit();
            }            
        }

        [Test]
        public void CssPathElementDisplayed_Test()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com/selectors/css-path/");

            IWebElement element = driver.FindElement(By.CssSelector("#post-108 > div > figure > img"));

            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed);
            driver.Quit();
        }

        [Test]
        public void ClassNameElementDisplayed_Test()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com/selectors/class-name/");

            IWebElement element = driver.FindElement(By.ClassName("testClass"));

            Thread.Sleep(1000);
            if (element.Text == "This is a paragraph with text that belongs to a class.")
            {
                Assert.Pass(element.Text);
                driver.Quit();
            }
            else
            {
                Assert.Fail(element.Text);
                driver.Quit();
            }
        }

        [Test]
        public void IdElementDisplayed_Test()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com/selectors/id/");

            IWebElement element = driver.FindElement(By.Id("testImage"));

            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed);

            driver.Quit();
        }

        [Test]
        public void NameElementDisplayed_Test()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com/selectors/name");

            IWebElement element = driver.FindElement(By.Name("myName"));

            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed);

            driver.Quit();
        }

        [Test]
        public void NavigateToURL_Test()
        {

            driver.Navigate().GoToUrl("http://testing.todorvachev.com");

            var driverUrl = driver.Url;

            Thread.Sleep(1000);
            Assert.IsNotEmpty(driverUrl);

            driver.Quit();
        }
    }
}
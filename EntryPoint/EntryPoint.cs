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
        static IWebDriver driver;
        static IWebElement element;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(@"C:\Drivers\chromedriver_win32\");            
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/text-input-field/", "Test text")]
        public void TextBoxInput_Test(string url, string testText)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.Name("username"));

            
            element.SendKeys(testText);
            Thread.Sleep(3000);

            var returnInput = element.GetAttribute("value");

            Assert.That(testText, Is.EqualTo(returnInput).NoClip);
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todorvachev.com/selectors/css-path/")]
        public void XPathlementDisplayed_Test(string url)
        {
            driver.Navigate().GoToUrl(url);            

            try
            {
                element = driver.FindElement(By.XPath("//*[@id=\"post-108\"]/div/figure/img")); // Use only if you have no other choice

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
        [TestCase("http://testing.todorvachev.com/selectors/css-path/")]
        public void CssPathElementDisplayed_Test(string url)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.CssSelector("#post-108 > div > figure > img")); // Use only if you have no other choice, higher performace (faster) than XPath

            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed);
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todorvachev.com/selectors/class-name/", "This is a paragraph with text that belongs to a class.")]
        public void ClassNameElementDisplayed_Test(string url, string textToCompare)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.ClassName("testClass"));

            Thread.Sleep(1000);          

            Assert.That(element.Text, Is.EqualTo(textToCompare));
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todorvachev.com/selectors/id/")]
        public void IdElementDisplayed_Test(string url)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.Id("testImage"));    // ID and Name is the best scenario

            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed);

            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todorvachev.com/selectors/name")]
        public void NameElementDisplayed_Test(string url)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.Name("myName"));    // ID and Name is the best scenario

            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed);

            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todorvachev.com")]
        public void NavigateToURL_Test(string url)
        {

            driver.Navigate().GoToUrl(url);

            var driverUrl = driver.Url;

            Thread.Sleep(1000);
            Assert.IsNotEmpty(driverUrl);

            driver.Quit();
        }       
    }
}
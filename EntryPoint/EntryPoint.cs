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
        static IAlert alert;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(@"C:\Drivers\chromedriver_win32\");            
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/alert-box/")]
        public void AlertBoxConfirm_Test(string url)
        {
            driver.Navigate().GoToUrl(url);

            alert = driver.SwitchTo().Alert();
            alert.Accept();

            Thread.Sleep(3000);
            try
            {
                var textOfUnexistingAlert = alert.Text;
            }
            catch (NoAlertPresentException e)
            {
                Assert.Pass(e.Message);
            }
            finally
            {
                driver.Quit();
            }            
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/alert-box/", "Hello! I am Alert Box! Click \"OK\" to dismiss me!")]
        public void AlertBoxPresent_Test(string url, string text)
        {
            driver.Navigate().GoToUrl(url);

            alert = driver.SwitchTo().Alert();
            
            Thread.Sleep(3000);

            Assert.That(text, Is.EqualTo(alert.Text));
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/drop-down-menu-test/", "1", "volvo")]
        [TestCase("http://testing.todvachev.com/special-elements/drop-down-menu-test/", "2", "saab")]
        [TestCase("http://testing.todvachev.com/special-elements/drop-down-menu-test/", "3", "mercedes")]
        [TestCase("http://testing.todvachev.com/special-elements/drop-down-menu-test/", "4", "audi")]
        public void DropdownMenuSelectItems_Test(string url, string option , string value)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.CssSelector("#post-6 > div > p:nth-child(6) > select > option:nth-child(" + option + ")"));
            element.Click();
            var isSelected = element.GetAttribute("value");

            Thread.Sleep(3000);

            Assert.That(value, Is.EqualTo(isSelected));
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/drop-down-menu-test/")]
        public void DropdownMenuInitValue_Test(string url)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.Name("DropDownTest"));

            var isInit = element.GetAttribute("value");
            Thread.Sleep(3000);

            Assert.That("Volvo", Is.EqualTo(isInit));
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/radio-button-test/", "1")]
        [TestCase("http://testing.todvachev.com/special-elements/radio-button-test/", "3")]
        [TestCase("http://testing.todvachev.com/special-elements/radio-button-test/", "5")]
        public void RadioButtonInput_Test(string url, string option)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.CssSelector("#post-10 > div > form > p:nth-child(6) > input[type=radio]:nth-child(" + option + ")"));

            var isChecked = element.GetAttribute("checked");
            Thread.Sleep(3000);

            Assert.That("true", Is.EqualTo(isChecked));
            driver.Quit();
        }

        [Test]
        [TestCase("http://testing.todvachev.com/special-elements/check-button-test-3/", "1")]
        [TestCase("http://testing.todvachev.com/special-elements/check-button-test-3/", "3")]
        public void CheckBoxInput_Test(string url, string option)
        {
            driver.Navigate().GoToUrl(url);

            element = driver.FindElement(By.CssSelector("#post-33 > div > p:nth-child(8) > input[type=checkbox]:nth-child(" + option + ")"));

            var isChecked = element.GetAttribute("checked");
            Thread.Sleep(3000);

            Assert.That("true", Is.EqualTo(isChecked));
            driver.Quit();
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
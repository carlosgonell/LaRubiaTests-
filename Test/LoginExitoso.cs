using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace LaRubiaTests.Tests
{
    public class LoginExitoso
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new EdgeOptions();

            string driverPath = @"C:\WebDrivers\";

            driver = new EdgeDriver(driverPath, options);
            driver.Navigate().GoToUrl("http://localhost:8000");
        }

        [Test]
        public void LoginCorrecto()
        {
            driver.FindElement(By.Name("usuario")).SendKeys("demo");
            driver.FindElement(By.Name("clave")).SendKeys("tareafacil25"); 
            driver.FindElement(By.TagName("button")).Click();

            Assert.That(driver.PageSource.Contains("Bienvenido"), Is.True);
        }

        [TearDown]
        public void Teardown()
        {
            driver?.Dispose();
        }
    }
}

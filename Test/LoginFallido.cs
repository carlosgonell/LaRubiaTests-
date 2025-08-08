using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;

namespace LaRubiaTests.Tests
{
    public class LoginFallido
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
        public void LoginConCredencialesIncorrectas()
        {
            driver.FindElement(By.Name("usuario")).SendKeys("demo");
            driver.FindElement(By.Name("clave")).SendKeys("clave_incorrecta"); 
            driver.FindElement(By.TagName("button")).Click();

            Assert.That(driver.PageSource.Contains("Usuario o clave incorrecta."), Is.True);
        }

        [TearDown]
        public void Teardown()
        {
            driver?.Dispose();
        }
    }
}

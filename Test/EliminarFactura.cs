using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace LaRubiaTests.Tests
{
    public class EliminarFactura
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new EdgeOptions();
            string driverPath = @"C:\WebDrivers\";

            driver = new EdgeDriver(driverPath, options);
            driver.Navigate().GoToUrl("http://localhost:8000");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Login
            wait.Until(driver => driver.FindElement(By.Name("usuario"))).SendKeys("demo");
            driver.FindElement(By.Name("clave")).SendKeys("tareafacil25");
            driver.FindElement(By.TagName("button")).Click();

            wait.Until(driver => driver.PageSource.Contains("Bienvenido"));
        }

        [Test]
        public void EliminarFacturaDesdeListado()
        {
            // Ir a la página de facturas
            driver.Navigate().GoToUrl("http://localhost:8000/ver_facturas.php");

            wait.Until(driver => driver.FindElement(By.TagName("table")));

            // Buscar botón de eliminar por href
            var eliminarBtn = wait.Until(driver =>
                driver.FindElement(By.XPath("//a[contains(@href, 'eliminar_factura.php?id=')]"))
            );

            // Hacer clic en el botón
            eliminarBtn.Click();

            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();

            Thread.Sleep(2000);

            Assert.That(driver.Url.Contains("ver_facturas.php"), Is.True, "No volvió a la página de facturas");

            Assert.Pass("Factura eliminada correctamente.");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Dispose();
        }
    }
}

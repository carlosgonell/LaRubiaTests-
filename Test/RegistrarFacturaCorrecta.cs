using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;

namespace LaRubiaTests.Tests
{
    public class RegistrarFacturaCorrecta
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            var options = new EdgeOptions();
            string driverPath = @"C:\WebDrivers\";

            driver = new EdgeDriver(driverPath, options);
            driver.Navigate().GoToUrl("http://localhost:8000");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // LOGIN
            wait.Until(driver => driver.FindElement(By.Name("usuario"))).SendKeys("demo");
            driver.FindElement(By.Name("clave")).SendKeys("tareafacil25");
            driver.FindElement(By.TagName("button")).Click();

            wait.Until(driver => driver.PageSource.Contains("Bienvenido"));

            // Ir a registrar_factura.php
            driver.Navigate().GoToUrl("http://localhost:8000/registrar_factura.php");

            wait.Until(driver => driver.FindElement(By.Name("codigo_cliente")));
        }

        [Test]
        public void RegistroFacturaExitoso()
        {
            // Llenar el formulario
            driver.FindElement(By.Name("codigo_cliente")).SendKeys("CL001");
            driver.FindElement(By.Name("nombre_cliente")).SendKeys("Cliente Test");
            driver.FindElement(By.Name("comentario")).SendKeys("Factura automatizada");

            // Artículos
            driver.FindElement(By.Name("articulos[0][nombre]")).SendKeys("Producto X");
            driver.FindElement(By.Name("articulos[0][cantidad]")).SendKeys("2");
            driver.FindElement(By.Name("articulos[0][precio]")).SendKeys("150.00");

            // Enviar
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(driver => driver.Url.Contains("imprimir_recibo.php"));

            Assert.That(driver.Url.Contains("imprimir_recibo.php"), Is.True, "No redirigió al recibo correctamente");
        }

        [TearDown]
        public void Teardown()
        {
            driver?.Dispose();
        }
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;


namespace LaRubiaTests.Tests
{
    public class RegistrarFacturaCantidadInvalida
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            var options = new EdgeOptions();
            string driverPath = @"C:\WebDrivers\";
            driver = new EdgeDriver(driverPath, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost:8000");

            // Login primero
            driver.FindElement(By.Name("usuario")).SendKeys("demo");
            driver.FindElement(By.Name("clave")).SendKeys("tareafacil25");
            driver.FindElement(By.TagName("button")).Click();

            // Ir al formulario de registrar factura
            driver.Navigate().GoToUrl("http://localhost:8000/registrar_factura.php");
        }

        [Test]
        public void RegistroConCantidadInvalida()
        {
            driver.FindElement(By.Name("codigo_cliente")).SendKeys("CL001");
            driver.FindElement(By.Name("nombre_cliente")).SendKeys("Cliente Inválido");
            driver.FindElement(By.Name("comentario")).SendKeys("Factura con cantidad cero");

            driver.FindElement(By.Name("articulos[0][nombre]")).SendKeys("Producto Prueba");
            driver.FindElement(By.Name("articulos[0][cantidad]")).Clear();
            driver.FindElement(By.Name("articulos[0][cantidad]")).SendKeys("0");
            driver.FindElement(By.Name("articulos[0][precio]")).SendKeys("50");

            // Click en el botón Guardar
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            string alertText = alert.Text;
            Console.WriteLine("Mensaje del alert: " + alertText);

            // Asegurarse de que el mensaje sea el esperado
            Assert.That(alertText.Contains("cantidad debe ser mayor que 0"), Is.True);

            alert.Accept(); // cerrar el alert
        }

        [TearDown]
        public void Teardown()
        {
            driver?.Dispose();
        }
    }
}

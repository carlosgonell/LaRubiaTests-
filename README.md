Proyecto de Pruebas Automatizadas – LaRubiaTests
Este proyecto contiene las pruebas automatizadas del sistema La Rubia, desarrolladas en C# usando Selenium WebDriver y NUnit.
El objetivo es validar funcionalidades clave del sistema, incluyendo login, registro de facturas y eliminación de facturas.



Estructura del proyecto:


📂 Carpeta raíz (LaRubiaTests)
Contiene la configuración principal del proyecto y las carpetas con código y reportes.

LaRubiaTests.csproj → Archivo de configuración del proyecto C# (.NET).

trx-to-html.xsl → Plantilla usada para convertir el reporte .trx generado por NUnit a HTML.

README.md → Este documento con la explicación del proyecto.



📂 Carpeta Test/:
Contiene todos los archivos con las pruebas automatizadas.

Cada prueba está en un archivo independiente y usa EdgeDriver desde la ruta C:\WebDrivers\:

LoginExitoso.cs → Verifica que un usuario pueda iniciar sesión correctamente.

LoginFallido.cs → Valida el mensaje de error al ingresar credenciales incorrectas.

RegistrarFacturaCorrecta.cs → Prueba el registro exitoso de una factura.

RegistrarFacturaInvalida.cs → Valida que no se pueda registrar una factura con datos inválidos.

EliminarFacturaDesdeListado.cs → Prueba la eliminación de facturas desde la tabla de facturas.



📂 Carpeta Reports/:
Guarda el reporte final en formato HTML ya listo para abrir en el navegador, junto con los recursos necesarios para que se vea con estilos.

TestReport.html → Reporte final de pruebas automatizadas.

Archivos .css, .js e íconos .svg → Recursos para el estilo y funcionalidad del reporte.



Flujo de ejecución de las pruebas:
Se ejecutan las pruebas automatizadas desde el proyecto.

Se genera un archivo de resultados en formato .trx.

Este archivo se convierte a HTML usando la plantilla incluida (trx-to-html.xsl).

El HTML resultante se guarda en la carpeta Reports con el nombre TestReport.html.

El reporte HTML puede abrirse en cualquier navegador para visualizar los resultados con un formato claro y estilizado.




Requisitos para la ejecución:
Tener instalado Microsoft Edge.

Tener Microsoft Edge WebDriver ubicado en C:\WebDrivers\.

El sistema La Rubia debe estar en funcionamiento en la dirección http://localhost:8000 al momento de ejecutar las pruebas. Este es el comando para poner el servidor local de php para correr: php -S localhost:8000. Este es el servidor de la aplicacion de LaRubia.

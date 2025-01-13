Proyecto ASP.NET Core Web App Razor Pages con MySQL

Este proyecto es una aplicación web basada en ASP.NET Core Razor Pages conectada a una base de datos MySQL. Incluye autenticación básica y la capacidad de gestionar tests con preguntas y respuestas.

✅ Herramientas necesarias (resumen):------------------
.NET SDK (Última versión compatible con tu proyecto)

Descargar desde: https://dotnet.microsoft.com/download
Visual Studio Code (o Visual Studio 2022)

Descargar desde: https://code.visualstudio.com/
MySQL Server y MySQL Workbench

MySQL Server: Motor de base de datos.
MySQL Server 8.0.32 o superior

MySQL Workbench: Herramienta visual para gestionar la base de datos.
MySQL Workbench 8.0.32 o superior

Descargar desde: https://dev.mysql.com/downloads/
Paquetes NuGet (Entity Framework Core y MySQL)

Instalar con los comandos:
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.2
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0

---------------------------.


---- opcional
📦 Configuración Inicial del Proyecto:

✅ 1. Crear un Nuevo Proyecto ASP.NET Core Web App Razor Pages

dotnet new razor -n test_app
cd test_app

✅ 2. Restaurar Paquetes NuGet

dotnet restore
-------------------o



✅  Configurar la Conexión a MySQL en appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=infopunk;User=root;Port=3306;"
  }
}

✅  Agregar Paquetes Necesarios (Entity Framework Core y MySQL)

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package MySql.Data.EntityFrameworkCore

📊 Creación de la Base de Datos y Migraciones:

✅ Crear la Migración Inicial y Actualizar la Base de Datos

dotnet ef migrations add InitialCreate
dotnet ef database update

🚀 Ejecución del Proyecto:

✅ 6. Ejecutar el Servidor Web (Cliente y Servidor Juntos)

dotnet run

✅ 7. Verificar la URL de Ejecución

La terminal mostrará un mensaje como:

Now listening on: http://localhost:5000

Accede a esa URL desde tu navegador.

📦 Actualización de Migraciones y Base de Datos:

Si realizas cambios en los modelos (por ejemplo: Test, User, Question) debes actualizar la base de datos con los siguientes comandos:

dotnet ef migrations add UpdateModels
dotnet ef database update

✅ Si deseas eliminar la última migración creada:

dotnet ef migrations remove

🔥 Resolución de Problemas Comunes:

1. Error con MySQL o Migraciones:

Asegúrate de que MySQL Server esté en ejecución.

Usa el comando con más detalles:

dotnet run --verbosity detailed

2. Error al Guardar Cambios en la Base de Datos:

Verifica que las claves foráneas estén correctamente definidas.

Usa el siguiente flujo para reiniciar la base de datos completamente:

dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update

📌 Cuándo Ejecutar los Comandos:

Después de cambios en los modelos: (añadir o eliminar columnas, relaciones)

Si la base de datos ha sido eliminada o restaurada.

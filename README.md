# TransporterApp

## Descripción
Este proyecto implementa una solución ASP.NET Core con dos capas:
- Transporter.Web (MVC): aplicación con vistas Razor, autenticación por cookies y CRUD protegido.
- Transporter.API (Web API): expone endpoints REST protegidos con JWT y documentados en Swagger.
Implementado así con el objetivo de demostrar autenticación en ambas capas y separación de responsabilidades.

## Requisitos previos
- .NET 8 SDK
- SQL Server LocalDB (o la base configurada en appsettings.json)
- Visual Studio 2022 / VS Code

## Configuración
- Clonar el repositorio.
- Revisar appsettings.json en Transporter.API y Transporter.Web para confirmar la cadena de conexión.
- Ejecutar migraciones (si aplica):
  
    dotnet ef database update --project Transporter.Data


## Credenciales de prueba
- **Usuario:** admin
- **Contraseña:** 1234

## Uso en MVC (Transporter.Web)
- Establecer Transporter.Web como proyecto de inicio.
- Ejecutar la aplicación.
- Al ingresar redirige automáticamente a /Account/Login.
- Ingresar credenciales.
- Acceder al CRUD de Transporter.

## Uso en API (Transporter.API)
- Establecer Transporter.API como proyecto de inicio.
- Al ejecutar la aplicación swagger debe abrir automáticamente.
- En POST /api/Auth/login, enviar:
  
    {
      "username": "admin",
      "password": "1234"
    }
  
- Copiar el token devuelto.
- Hacer clic en Authorize (Botón con candado en Swagger) y pegar:
    Bearer <token-generado>
- Probar endpoints protegidos como GET /api/Transporters.
- Endpoint de consumo de servicio externo "with-products" debe retornar los valores de transporter existentes y productos obtenidos de servicio FakeStoreApi

## Notas técnicas
- MVC usa cookies para autenticación (Implementación temporal dada restricción con cuenta Azure para la implementación de B2C y Entra External ID)
- API usa JWT con HS256 y clave de ≥32 caracteres.
- Endpoints de login (/Account/Login en MVC y /api/Auth/login en API) están marcados con [AllowAnonymous].
- Endpoints de negocio (Transporters) están protegidos con [Authorize].

## Futuro / Producción
- Migrar autenticación a Microsoft Entra External ID (sucesor de Azure AD B2C).
- Guardar la clave JWT en Azure Key Vault o variables de entorno.
- Implementar roles y claims para autorización más granular.

## Validación rápida
- [ ] MVC redirige a /Account/Login si no hay sesión
- [ ] Login con admin/1234 permite acceder al CRUD
- [ ] API devuelve 401 sin token
- [ ] API devuelve datos con token válido
- [ ] Endpoint /api/Transporters/with-products responde con datos combinados

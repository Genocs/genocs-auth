# dotnet-6-signup-verification-api

.NET 6.0 - Boilerplate API with Email Sign Up, Verification, Authentication & Forgot Password

Documentation at https://jasonwatmore.com/post/2022/02/26/net-6-boilerplate-api-tutorial-with-email-sign-up-verification-authentication-forgot-password

Documentación en español en https://jasonwatmore.es/post/2022/02/26/net-6-tutorial-de-api-estandar-con-registro-de-correo-electronico-verificacion-autenticacion-y-contrasena-olvidada


### Install dotnet ef tools
The .NET Entity Framework Core tools (dotnet ef) are used to generate EF Core migrations, to install the EF Core tools globally run
``` PS
dotnet tool install -g dotnet-ef
``` 
, or to update run
 
``` PS
dotnet tool update -g dotnet-ef
```

``` PS
cd Genocs.Auth.WebApi
dotnet ef migrations add InitialCreate

```

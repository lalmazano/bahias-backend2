using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using Shared.Security.Encrypt;
using Shared.Security.JWT.Interface;
using Shared.Security.JWT;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin() // Permite cualquier origen
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
#region Registrar Servicios 

//-------------------------------------------------------------------------------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------------------------------------------------------------------------------//
#endregion

#region Registrar Repositorios

//-------------------------------------------------------------------------------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------------------------------------------------------------------------------//
#endregion

#region Conexion a la base de datos
//Conexion a la base de datos//
//-------------------------------------------------------------------------------------------------------------------------------------------------------//
var encriptador = new DataEncriptada();
// Registrar QueryContext para consultas
builder.Services.AddDbContext<QueryContext>(options =>
{
    options.UseOracle(encriptador.DesencriptarData(configuration.GetConnectionString("OracleQueryUser")), x => x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23));
});

// Registrar OperationContext para operaciones
builder.Services.AddDbContext<OperationContext>((serviceProvider, options) =>
{
    //var interceptor = serviceProvider.GetRequiredService<DbConnectionInterceptor>();
    options.UseOracle(encriptador.DesencriptarData(configuration.GetConnectionString("OracleOperationUser")), x => x.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion23));
    //options.AddInterceptors(interceptor);
});
//-------------------------------------------------------------------------------------------------------------------------------------------------------//
#endregion


# region Registro el servicio de JWT
//-------------------------------------------------------------------------------------------------------------------------------------------------------//

var key = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(key))
{
    Console.WriteLine("Error: La clave JWT no está configurada.");
    throw new Exception("La clave JWT es nula o vacía. Verifica la configuración en appsettings.json.");
}
// Registrar el servicio de JWT
builder.Services.AddScoped<IJwtService>(provider => new JwtService(key));
// Configurar autenticación JWT
var keyBytes = Encoding.ASCII.GetBytes(key);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false, // Cambia a true si necesitas un emisor específico
        ValidateAudience = false, // Cambia a true si necesitas una audiencia específica
        ValidateLifetime = true // Verifica si el token está expirado
    };
});

// Habilitar autorización global
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser() // Requiere que todos los usuarios estén autenticados
        .Build();
});

//-------------------------------------------------------------------------------------------------------------------------------------------------------//
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Configuriacion Swager 
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT en el formato: Bearer {token} (el prefijo 'Bearer' se agrega automáticamente).",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

app.MapGet("/", () => Results.Json(new { Version = "1.0.0" })).AllowAnonymous(); ;
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

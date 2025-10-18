using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Text;
using Shared.Security.Encrypt;
using Shared.Security.JWT.Interface;
using Shared.Security.JWT;
using Application.Services.Interface;
using Application.Services;
using Infrastructure.Repositories.Interface;
using Infrastructure.Repositories;
using Api.Mapping;
using Microsoft.Extensions.DependencyInjection;

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
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUserRolService, UserRolService>();
builder.Services.AddScoped<IDataEncriptada, DataEncriptada>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IEstadoBahiasService, EstadoBahiasService>();
builder.Services.AddScoped<IBahiaService,BahiaService >();
builder.Services.AddScoped<IEstadoReservaService,EstadoReservaService>();
builder.Services.AddScoped<IParametroService,ParametroService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<ITipoVehiculoService,TipoVehiculoService>();
builder.Services.AddScoped<IUbicacionService,UbicacionService>();
builder.Services.AddScoped<IVehiculoService,VehiculoService>();
//-------------------------------------------------------------------------------------------------------------------------------------------------------//
#endregion

#region Registrar Repositorios

//-------------------------------------------------------------------------------------------------------------------------------------------------------//
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUserRolRepository, UserRolRepository>();
builder.Services.AddScoped<IEstadoBahiasRepository, EstadoBahiasRepository>();
builder.Services.AddScoped<IBahiasRepository,BahiasRepository >();
builder.Services.AddScoped<IEstadoReservaRepository, EstadoReservaRepository>();
builder.Services.AddScoped<IParametroRepository,ParametroRepository>();
builder.Services.AddScoped<IReservaRepository,ReservaRepository>();
builder.Services.AddScoped<ITipoVehiculoRepository,TipoVehiculoRepository>();
builder.Services.AddScoped<IUbicacionRepository,UbicacionRepository>();
builder.Services.AddScoped<IVehiculoRepository,VehiculoRepository>();
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
    Console.WriteLine("Error: La clave JWT no est� configurada.");
    throw new Exception("La clave JWT es nula o vac�a. Verifica la configuraci�n en appsettings.json.");
}
// Registrar el servicio de JWT
builder.Services.AddScoped<IJwtService>(provider => new JwtService(key));
// Configurar autenticaci�n JWT
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
        ValidateIssuer = false, // Cambia a true si necesitas un emisor espec�fico
        ValidateAudience = false, // Cambia a true si necesitas una audiencia espec�fica
        ValidateLifetime = true // Verifica si el token est� expirado
    };
});

// Habilitar autorizaci�n global
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser() // Requiere que todos los usuarios est�n autenticados
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
        Description = "Ingrese el token JWT en el formato: Bearer {token} (el prefijo 'Bearer' se agrega autom�ticamente).",
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

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
    app.UseSwagger();
    app.UseSwaggerUI();

#endregion

app.MapGet("/", () => Results.Json(new { Version = "1.0.0" })).AllowAnonymous(); ;
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

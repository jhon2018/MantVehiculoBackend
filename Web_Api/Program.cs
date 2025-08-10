using AccesoDatos.Models;
using AccesoDatos.Operations;
using AccesoDatos.Plugins;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Web_Api.Controllers;
using Web_Api.Services;
//using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);


//RENDER
var contentRoot = builder.Environment.ContentRootPath;
//var jsonPath = Path.Combine(contentRoot, "checklists.json");
var jsonPath = Path.Combine(contentRoot, "MockData", "checklists.json");
var jsonString = File.ReadAllText(jsonPath);
// luego lo deserializas con JsonConvert o System.Text.Json



// El sistema está intentando crear el UsuarioController,registrar el servicio manualmente
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioDAO>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<VehiculoDAO>();
builder.Services.AddScoped<ConductorService>();
builder.Services.AddScoped<ConductorDAO>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<ProveedorDAO>();
builder.Services.AddScoped<MantenimientoDAO>();
builder.Services.AddScoped<TipoReparacionService>();
builder.Services.AddScoped<TipoReparacionDAO>();
builder.Services.AddScoped<DetalleReparacionDAO>();
builder.Services.AddScoped<DetalleReparacionService>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//23/07
//JWT
var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta"); // Obtener la clave JWT desde appsettings.json

//JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer. <<\r\n\n" +
        "Ingresa la palabra 'Bearer' seguido de un espacio y después su token en el campo de abajo.r\n\r\n" +
        "Ejemplo: \"Bearer adqweqdskdfl\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
}
);

builder.Services.AddAuthorization
    (
        options =>
        {
            options.AddPolicy("jonathan", policy =>
            policy.RequireClaim("Nombre", "jonathan"));
        }
    );


//JWT TE VAS appsetting.json
builder.Services.AddAuthentication
    (
        x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer
    (
        x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    );




//Ajusta tu política CORS para permitir múltiples orígenes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteFrontend", policy =>
    {
        policy.WithOrigins(
            "https://localhost:5173",
            "https://127.0.0.1:5173",
            "https://192.168.40.97:5173",
            "https://192.168.6.1:5173",
            "https://jonathanvs-001-site1.mtempurl.com",
            "https://jonathanvs-001-site1.mtempurl.com/webapp",
             "https://piloto-mantenimiento-vehiculo.web.app" // ← ¡Agrega esto! firebase
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


//Configura Kestrel manualmente
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7157); // Escucha en todas las IPs
//});

//se use automáticamente cuando publiques.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.UseSwagger();
//    //app.UseSwaggerUI();


   

//}


app.UseSwagger();//produccion
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
});


// Usar CORS (antes de Authorization)
app.UseCors("AllowViteFrontend");




//app.UseHttpsRedirection();

app.UseAuthentication(); // Asegúrate de que la autenticación se configure antes de la autorización
app.UseAuthorization();

app.MapControllers();

app.Run();


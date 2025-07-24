using AccesoDatos.Operations;
using AccesoDatos.Plugins;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Web_Api.Services;
//using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// El sistema est� intentando crear el UsuarioController,registrar el servicio manualmente
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioDAO>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//23/07
//JWT
var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta"); // Obtener la clave JWT desde appsettings.json

//JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticaci�n JWT usando el esquema Bearer. <<\r\n\n" +
        "Ingresa la palabra 'Bearer' seguido de un espacio y despu�s su token en el campo de abajo.r\n\r\n" +
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




//Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // origen del frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS (antes de Authorization)
app.UseCors("AllowViteFrontend");

app.UseHttpsRedirection();

app.UseAuthentication(); // Aseg�rate de que la autenticaci�n se configure antes de la autorizaci�n
app.UseAuthorization();

app.MapControllers();

app.Run();


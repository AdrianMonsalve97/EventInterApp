using Api.Middlewares;
using Application;
using Application.Common.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infraestructure.Mailing;
using Infraestructure.Persistence;
using Infrastructure.Mailing.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Api.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configuración de conexión a la base de datos
builder.Services.AddDbContext<EventosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de AutoMapper, MediatR y Validaciones
builder.Services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);
builder.Services.AddMediatR(typeof(ApplicationAssemblyMarker).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPasswordExpirationService, PasswordExpirationService>();

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});




// Configuración de autenticación JWT
string? claveBase64 = builder.Configuration["Jwt:Key"];
if (claveBase64 is null)
{
    throw new InvalidOperationException("Debe configurar 'Jwt:Key'");
}

byte[] claveBytes = Convert.FromBase64String(claveBase64);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(claveBytes)
        };
    });


builder.Services.AddAuthorization();
builder.Services.AddControllers(); 


// Documentación Swagger con soporte para token Bearer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Gestión de Eventos",
        Version = "v1",
        Description = "API para la gestión de eventos con CQRS, autenticación JWT y roles"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token en formato: Bearer {su_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Api.xml"));
    options.DocumentFilter<ControllerSummaryDocumentFilter>();

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});





var app = builder.Build();

// Middleware de errores y validación de header personalizado
app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseMiddleware<ValidateClientHeaderMiddleware>();

// Activar Swagger solo en desarrollo
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gestión de Eventos v1");
    c.InjectJavascript("/swagger/custom-header.js");
});





app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Ejecutar semilla de datos (usuario administrador por defecto)
using (IServiceScope scope = app.Services.CreateScope())
{
    EventosDbContext context = scope.ServiceProvider.GetRequiredService<EventosDbContext>();
    await SeedData.InicializarAsync(context);
}

app.Run();

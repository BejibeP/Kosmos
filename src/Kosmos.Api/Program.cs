var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace OC
{
    public class Program
    {
        public static void Main(string[] args)
        {

#if DEBUG
            // Active les logs détaillés pour les tokens JWT
            IdentityModelEventSource.ShowPII = true;
#endif

            var builder = WebApplication.CreateBuilder(args);

            // Charge les appsettings en mémoire
            builder.Services.LoadConfiguration(builder.Configuration);

            // Configure l'authentification et l'authorization
            builder.Services.ConfigureAuthentication(builder.Configuration);

            // Add services, controllers and containers to the container.
            builder.Services.ConfigureDependencyInjection(builder.Configuration);

            // Configure Swagger/OpenAPI
            builder.Services.ConfigureSwaggerServices(builder.Configuration);

            var app = builder.Build();

            // configure swagger
            app.ConfigureSwaggerClient(builder.Configuration);

            // Route prefix
            app.ConfigureRouting(builder.Configuration);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

*/
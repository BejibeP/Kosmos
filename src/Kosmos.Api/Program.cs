using Bejibe.Kosmos.Api.Extensions.ServiceCollection;
using Bejibe.Kosmos.Api.Extensions.WebApplication;

var builder = WebApplication.CreateBuilder(args);

// Configure Application / Load Configuration from Sources
builder.Configuration.AddConfigurationSources();
builder.Services.LoadConfiguration(builder.Configuration);

// Configure Logging / Add custom logging management
builder.Logging.ConfigureLogging();

// Dependancy Injection

// Add Infra (BDD, Mailing, etc)
builder.Services.AddDatabase(builder.Configuration);

// Add Repositories, Business Service and Controllers (the order is required)
builder.Services.AddRepositories()
    .AddBusinessServices()
    .AddControllers();

// Add Swagger UI / OpenAPI

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/*

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

#if DEBUG
            // Active les logs détaillés pour les tokens JWT
            IdentityModelEventSource.ShowPII = true;
#endif

            // Configure l'authentification et l'authorization
            builder.Services.ConfigureAuthentication(builder.Configuration);

            // Configure Swagger/OpenAPI
            builder.Services.ConfigureSwaggerServices(builder.Configuration);

        }
    }
}

*/

// Build the WebApplication

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
Seulement si on veut la vrai IP et non le proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
*/

app.UseHttpsRedirection();

// configure it
app.UseCors();

// move ? ext ?
app.UseHttpMethodOverride();

// app.UseAuthentication();

app.UseAuthorization();

app.UseRequestMetadataMiddleware();

app.MapControllers();


app.Run();

/*
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
*/

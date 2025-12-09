using Bejibe.Kosmos.Api.Extensions.Application;
using Bejibe.Kosmos.Api.Extensions.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// Configure Application / Load Configuration from Sources
builder.Configuration.AddConfigurationSources();
builder.Services.LoadConfiguration(builder.Configuration);

// Configure Logging / Add custom logging management
builder.Logging.ConfigureLogging();

// Dependancy Injection

// Add Infra (BDD, Mailing, etc)
builder.Services.AddDatabase(builder.Configuration);

// Add Custom Authentication
builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddCorsPolicies(builder.Configuration);

// Add Repositories, Business Service and Controllers (the order is required)
builder.Services.AddRepositories()
    .AddBusinessServices()
    .AddControllers();

// Add Swagger UI / OpenAPI
builder.Services.AddSwaggerServices(builder.Configuration);

// Build the WebApplication

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerWebUI(builder.Configuration);

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

app.UseAuthentication();

app.UseAuthorization();

app.UseRequestMetadataMiddleware();

app.MapControllers();

app.Run();

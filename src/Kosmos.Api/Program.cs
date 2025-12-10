using Bejibe.Kosmos.Api.Extensions.Application;
using Bejibe.Kosmos.Api.Extensions.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// ---- Application Services Configuration ----

// ---- 1. Application Configuration ----
builder.Configuration.AddConfigurationSources();
builder.Services.LoadConfiguration(builder.Configuration);

// ---- 2. Logging Service Configuration ----
builder.Logging.ConfigureLogging();

// ---- 3. Infrastructure Services ----
builder.Services.AddDatabase(builder.Configuration);

// ---- 4. Authentication and Authorization Configuration ----
builder.Services.AddCustomAuthentication(builder.Configuration);

// ---- 5. Security Configuration ----
builder.Services.AddCorsPolicies(builder.Configuration);

// ---- 6. Application Services (Repository, Business, Controllers) ----
builder.Services.AddRepositories()
    .AddBusinessServices()
    .AddControllers();

// ---- 7. Swagger/OpenAPI Services ----
builder.Services.AddSwaggerServices(builder.Configuration);


// ---- WebApplication Building and Middleware Setup ----
var app = builder.Build();

// order of middlewares
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index/_static/middleware-pipeline.svg?view=aspnetcore-6.0

// ---- 1. Exception Handling ----
// app.UseExceptionHandler();

// ---- 2. Security and redirection Services ----

// app.UseHsts();
app.UseHttpsRedirection();

app.UseRouting();

// configure it (precise policy)
app.UseCors();

app.UseHttpMethodOverride();

// app.UseForwardedHeaders(); //  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto

// ---- 3. Authentication and Authorization ----

app.UseAuthentication();

app.UseAuthorization();

// ---- 4. Swagger UI / Open API ---- -> move to 3 ?
app.UseSwaggerWebUI(builder.Configuration);

// ---- 5. Custom middlewares ----
app.UseRequestMetadataMiddleware();

// ---- 6. Controller Mapping ----
app.MapControllers();

// ---- 7. Booting application ----
app.Run();

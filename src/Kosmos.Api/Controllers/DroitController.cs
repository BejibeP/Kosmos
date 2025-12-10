using Kosmos.Business;
using Kosmos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bejibe.Kosmos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroitController : ControllerBase
    {

        private readonly ILogger<DroitController> _logger;

        private readonly IDroitService _service;

        public DroitController(ILogger<DroitController> logger, IDroitService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<DroitDto>> GetAll()
        {
            _logger.LogTrace("Start GetAllDroits Controller");

            var result = await _service.GetAllDroits();

            _logger.LogTrace("End GetAllDroits Controller");
            return result;
        }

        [HttpGet("{id}")]
        public async Task<DroitDto> GetById(string id)
        {
            _logger.LogTrace("Start GetDroitById Controller");

            var result = await _service.GetDroitById(id);

            _logger.LogTrace("End GetDroitById Controller");
            return result;
        }

        [HttpPost]
        public async Task<object> Add([FromBody] DroitDto droit)
        {
            _logger.LogTrace("Start GetDroitById Controller");

            var result = await _service.AddDroit(droit);

            _logger.LogTrace("End GetDroitById Controller");
            return result;
        }

    }
}

/*
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

using Kosmos.Common.Configuration;

namespace Bejibe.Kosmos.Api.Extensions.ServiceCollection
{
    public static class ConfigurationExtensions
    {

        /// <summary>
        /// Add additionnal configuration source to the API
        /// For now only add a appsettings.passwords.json that is ignored by git 
        /// and only here for local use to secure password and secrets
        /// </summary>
        /// <param name="configuration"></param>
        public static void AddConfigurationSources(this ConfigurationManager configuration)
        {
            configuration.AddJsonFile("appsettings.passwords.json", optional: true, reloadOnChange: true);
        }

        /// <summary>
        /// Load application configuration from AppSettings.json to "options" pattern based objects
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationManager"></param>
        public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var apiOptions = configurationManager.GetSection(ApiOptions.SectionName);
            services.Configure<ApiOptions>(apiOptions);

            var securityOptions = configurationManager.GetSection(SecurityOptions.SectionName);
            services.Configure<SecurityOptions>(securityOptions);

            var databasesOptions = configurationManager.GetSection(DatabasesOptions.SectionName);
            services.Configure<DatabasesOptions>(databasesOptions);

            var messagingOptions = configurationManager.GetSection(MessagingsOptions.SectionName);
            services.Configure<MessagingsOptions>(messagingOptions);

            var cachingOptions = configurationManager.GetSection(CachingOptions.SectionName);
            services.Configure<CachingOptions>(cachingOptions);

            var mailingOptions = configurationManager.GetSection(MailingsOptions.SectionName);
            services.Configure<MailingsOptions>(mailingOptions);

            var externalApisOptions = configurationManager.GetSection(ExternalAPIsOptions.SectionName);
            services.Configure<ExternalAPIsOptions>(externalApisOptions);


            var copy = apiOptions.Get<ApiOptions>();
            var copy1 = securityOptions.Get<SecurityOptions>();
            var copy2 = databasesOptions.Get<DatabasesOptions>();
            var copy3 = messagingOptions.Get<MessagingsOptions>();
            var copy4 = cachingOptions.Get<CachingOptions>();
            var copy5 = mailingOptions.Get<MailingsOptions>();
            var copy6 = externalApisOptions.Get<ExternalAPIsOptions>();

        }

    }
}
*/
namespace Wallet.Tracker.Api;

using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Wallet.Tracker.Api.Services;
using Wallet.Tracker.API.Middlewares;
using Wallet.Tracker.Domain.Services.Extensions;
using Wallet.Tracker.Infrastructure.CoinMarketCap.Extensions;
using Wallet.Tracker.Infrastruction.ChainExplorer.Extensions;
using Wallet.Tracker.Infrastructure.Telegram.Extensions;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddLogging(s => s.AddAWSProvider().Configure(s => s.ActivityTrackingOptions = ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId));

        services.AddControllers().AddNewtonsoftJson();

        services.AddDomainServices(Configuration);
        services.AddInfrastructureServices();
        services.AddCoinMarketCapServices(Configuration);
        services.AddChainExplorerServices(Configuration);
        services.AddTelegramBotServices(Configuration);

        services.AddTransient<ISqsClient, SqsClient>();

        services.AddHostedService<MigrationService>();

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
        };
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<TraceIdentifierLoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome. DB connection string: " + Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            });
        });
    }
}
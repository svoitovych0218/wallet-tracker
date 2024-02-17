namespace Wallet.Tracker.Api;

using Wallet.Tracker.API.Middlewares;
using Wallet.Tracker.Domain.Services.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        services.AddControllers();

        services.AddDomainServices(Configuration);
        services.AddInfrastructureServices();

        services.AddHostedService<Wallet.Tracker.Api.MigrationService>();

        //services.AddAuthentication(x =>
        //{
        //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = "Wallet",
        //        ValidAudience = "Wallet",
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JwtToken:JwtEncryptionKey")))
        //    };
        //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
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

        //app.UseAuthentication();
        //app.UseAuthorization();

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
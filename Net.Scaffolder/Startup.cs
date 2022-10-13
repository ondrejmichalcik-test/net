using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodeNow.Logging;
using CodeNow.Tracing;
using CodeNow.Tracing.HeaderPropagation;
using CodeNow.Metrics;
using CodeNow.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Net.Scaffolder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.ReplaceCodeNowEnvVariables();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddSingleton<JsonSerializer>();
            services.AddHealthChecks();
            services.AddCodeNowHttpContextAccessor();
            services.AddCodeNowTracing(builder =>
            {
                // see https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry/README.md#activity-source
                builder.AddSource("*");
            });

            //USE CLIENT ONLY THIS WAY !! TO PROPAGATE TRACING HEADERS !!
            /*services.AddHttpClient<IWebapiClient, WebapiClient>(c =>
            {
                c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ApiLocation"));
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddCodeNowHeaderPropagation();
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCodeNowHeaderPropagation();
            app.UseCodeNowHttpMetrics();
            app.UseCodeNowTracing();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net.Scaffolder V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapCodeNowsMetrics();
            });
        }
    }
}

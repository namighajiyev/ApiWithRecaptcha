using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using reCAPTCHA.AspNetCore;

namespace ApiWithRecaptcha
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc("api", new OpenApiInfo() { Title = "ApiWithRecaptcha", Version = "v1" });
                        options.CustomSchemaIds(y => y.FullName);
                        options.DocInclusionPredicate((version, apiDescription) => true);
                        options.TagActionsBy(y => new List<string>() { y.GroupName });
                    });

            services.AddCors((options) =>
                    {
                        options.AddDefaultPolicy((config) =>
                        {
                            config.AllowAnyHeader();
                            config.AllowAnyMethod();
                            config.AllowAnyOrigin();
                        });
                    });

            var reaptchaSecretKey = Configuration.GetValue<string>("ApiWithRecaptcha:RecaptchaSettings:SecretKey");
            var reaptchaSiteKey = Configuration.GetValue<string>("ApiWithRecaptcha:RecaptchaSettings:SiteKey");

            services.AddRecaptcha(options =>
                    {
                        options.SecretKey = reaptchaSecretKey;
                        options.SiteKey = reaptchaSiteKey;
                    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/api/swagger.json", "API");
            });

        }
    }
}

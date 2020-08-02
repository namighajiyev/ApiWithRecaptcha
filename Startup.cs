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
            var reaptchaV2TickBoxSecretKey = Configuration.GetValue<string>("ApiWithRecaptcha:V2TickboxRecaptchaSettings:SecretKey");
            var reaptchaV2TickBoxSiteKey = Configuration.GetValue<string>("ApiWithRecaptcha:V2TickboxRecaptchaSettings:SiteKey");
            var reaptchaV2InvisibleSecretKey = Configuration.GetValue<string>("ApiWithRecaptcha:V2InvisibleRecaptchaSettings:SecretKey");
            var reaptchaV2InvisibleSiteKey = Configuration.GetValue<string>("ApiWithRecaptcha:V2InvisibleRecaptchaSettings:SiteKey");

            services.Configure<RecaptchaOptions>(opt =>
            {
                opt.V3SecretKey = reaptchaSecretKey;
                opt.V3SiteKey = reaptchaSiteKey;
                opt.V2TickBoxSecretKey = reaptchaV2TickBoxSecretKey;
                opt.V2TickBoxSiteKey = reaptchaV2TickBoxSiteKey;
                opt.V2InvisibleSecretKey = reaptchaV2InvisibleSecretKey;
                opt.V2InvisibleSiteKey = reaptchaV2InvisibleSiteKey;
            });

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

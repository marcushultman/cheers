using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheers.Domain;
using Cheers.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Cheers.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddDbContext<CheersDbContext>(x =>
            {
                x.ConfigureDatabase(Configuration);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cheers API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
            });

            
            var provider = services.BuildServiceProvider();
            
            using (var dbContext = provider.GetRequiredService<CheersDbContext>())
            {
                var logger = provider.GetRequiredService<ILogger<Startup>>();
                var pending = dbContext.Database.GetPendingMigrations().ToList();
                if (pending.Count > 0)
                {
                    logger.LogWarning("Found pending migrations, applying...");
                    pending.ForEach(x => logger.LogInformation(x));
                    dbContext.Database.Migrate();
                }
            }
            
            //services.AddDbContextPool<CheersDbContext>(options =>
            //{
            //    options.ConfigureDatabase(Configuration);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cheers"));
            app.UseMvc();
        }
    }
}

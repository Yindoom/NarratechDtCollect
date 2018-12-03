using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;
using DtCollect.Core.Helpers;
using DtCollect.Core.Service;
using DtCollect.Core.Service.Impl;
using DtCollect.Infrastructure.Data;
using DtCollect.Infrastructure.Data.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MockHistorian;

namespace NarratechDtCollect
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddCors(options => options.AddPolicy("AllowAnyOrigin", builder => 
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddScoped<IRepo<User>, UserRepo>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IHistorian, MockIp21>();

            services.AddScoped<ISampleService, SampleService>();

            services.AddDbContext<DbContextDtCollect>(opt => opt.UseSqlite("Data Source=DtDatabase"));

            services.AddTransient<IDbSeed, DbSeed>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtSecurityKey.Key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                // Initialize the database
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<DbContextDtCollect>();
                var dbInitializer = services.GetService<IDbSeed>();
                dbInitializer.SeedDb(dbContext);
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
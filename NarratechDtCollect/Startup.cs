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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//comment
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

            //Sets the bytes for the AuthenticationHelper, as a random array of bytes.
            Byte[] secretBytes = new Byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);
            
            //Compatibility .Net version 2.1, so we're all working on the same dotnet
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            //Relations between objects in the database can cause infinite loops when sent as Json.
            //The JSONOptions below make sure we ignore loops when infinite loops are detected.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = 
                    new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore;
            });
            
            //THis is not secure, do not have this in a real product.
            //Allows any and all sources to access any methods with any headers, in the RestAPI
            services.AddCors(options => options.AddPolicy("AllowAnyOrigin", builder => 
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            //Dependency injection.
            services.AddScoped<IRepo<User>, UserRepo>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRepo<Log>, LogRepo>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IRepo<Request>, RequestRepo>();
            services.AddScoped<IRequestService, RequestService>();

            services.AddScoped<IHistorian, MockIp21>();

            services.AddScoped<ISampleService, SampleService>();

            services.AddDbContext<DbContextDtCollect>(opt => opt.UseSqlite("Data Source=DtDatabase"));

            //Injects a Seed for the database, as a one time use class
            services.AddTransient<IDbSeed, DbSeed>();
            
            //Singleton authenticationhelper, using the secret bytes instantiated at the beginning of configuration
            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));
            
            //Basic authentication, sets parameters for tokens
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            //When running, uses the Database Seed to seed the Database
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

            app.UseCors("AllowAnyOrigin");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
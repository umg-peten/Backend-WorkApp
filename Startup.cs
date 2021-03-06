using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkApp.Helpers;
using WorkApp.IServices;
using WorkApp.Models;
using WorkApp.Services;

namespace WorkApp
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
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConnection, Connection>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<ILogsWS, LogWS>();
            services.AddSingleton<ITokenHandler, Helpers.TokenHandler>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IPositionService, PositionService>();
            services.AddSingleton<IOvertimeService, OvertimeService>();
            services.AddSingleton<IClockingService, ClockingService>();
            services.AddHttpContextAccessor();

            //Json Web Token implementation
            var JWTSection = Configuration.GetSection("JWT");
            services.Configure<JWT>(JWTSection);

            // [ JWT ]
            var jwt = JWTSection.Get<JWT>();
            var secretKey = Encoding.ASCII.GetBytes(jwt.SigningKey);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(d =>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "mycors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
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
            app.UseCors("mycors");

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

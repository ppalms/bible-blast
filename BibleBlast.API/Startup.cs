using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Helpers;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace BibleBlast.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SqlServerAppContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            IdentityBuilder coreBuilder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
            });

            var identityBuilder = new IdentityBuilder(coreBuilder.UserType, typeof(Role), coreBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<SqlServerAppContext>();
            identityBuilder.AddRoleValidator<RoleValidator<Role>>();
            identityBuilder.AddRoleManager<RoleManager<Role>>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SHARED_KEY"))
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                });

            services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                });

            services.AddMvc(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    opt.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors();
            services.AddAutoMapper();
            services.AddTransient<Seeder>();
            services.AddTransient<IOrganizationProvider, OrganizationProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IKidRepository, KidRepository>();
            services.AddScoped<IMemoryRepository, MemoryRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seeder.Seed();
            }
            else
            {
                // app.UseHsts();
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.WithOrigins("http://localhost:4200")
                .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(routes => routes.MapSpaFallbackRoute(
                name: "spa-fallback",
                defaults: new { controller = "Fallback", action = "Index" }
            ));
        }
    }
}

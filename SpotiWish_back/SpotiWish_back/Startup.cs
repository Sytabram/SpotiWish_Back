using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SpotiWish_back.Configuration;
using SpotiWish_back.Data;
using SpotiWish_back.Repositories;
using SpotiWish_back.Repositories.Interface;
using SpotiWish_back.Services;
using SpotiWish_back.Services.Interface;


namespace SpotiWish_back
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            
            services.AddDbContext<SpotiWishDataContext>(x => x.UseSqlite(@"Data Source=SpotiWish.db;"));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SpotiWish_back", Version = "v1"});
            });
            services.AddTransient<IPlayListService, PlayListService>();
            services.AddTransient<IPlayListRepository, PlayListRepository>();
            
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 6;
                options.Password.RequiredLength = 12;
            }).AddEntityFrameworkStores<SpotiWishDataContext>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MedecinOnly", policy => policy.RequireClaim("IsMedecin", "True"));
                options.AddPolicy("CovidTestPolicy", policy => policy.Requirements.Add(new SamePatientRequirement()));
                options.AddPolicy("ChuvEmployee", policy => policy.Requirements.Add(new HospitalEmployeeRequirement("chuv.ch")));
            });

            services.AddSingleton<IAuthorizationHandler, SamePatientAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, HospitalEmployeeAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpotiWish_back v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
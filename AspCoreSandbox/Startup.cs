using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspCoreSandbox.Data;
using AspCoreSandbox.Data.Entities;
using AspCoreSandbox.Data.Extensions;
using AspCoreSandbox.Data.Repositories;
using AspCoreSandbox.Services;
using AutoMapper;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AspCoreSandbox
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(o => o.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<StoreDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication()
                    .AddCookie()
                    .AddJwtBearer(cfg => 
                    {
                        cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            NameClaimType = "name",
                            ValidIssuer = _configuration["Tokens:Issuer"],
                            ValidAudience = _configuration["Tokens:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]))
                        };
                    });

            var connectionString = _configuration["connectionStrings:StoreConnectionString"];
            services.AddDbContext<StoreDbContext>(o => o.UseSqlServer(connectionString));

            services.AddTransient<DbContextSeeder>();

            services.AddTransient<IMailService, NullMailService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemsRepository>();

            //Mapowanie mo¿na robiæ w lambdzie lub w klasie dziedzicz¹cej po profile
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(o => o.EnableEndpointRouting = false)
                    .AddXmlSerializerFormatters()
                    .AddNewtonsoftJson()
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0); //okreœlenie umo¿liwia z korzystania metadanych opisuj¹cych np. kontroler
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Middleware part

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseNodeModules();

            app.UseMvc(cfg => 
            {
                // Dodaj route - œcie¿ka, kontroler, akcja do wykonia
                // "/{Nazwa Kontrolera}/{Akcja d wykonania}/{id? - ? czyli opcjonalnie}" 


                cfg.MapRoute("Default", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
            });
        }
    }
}

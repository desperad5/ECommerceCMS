using ECommerceCMS.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Data.Repositories;
using ECommerceCMS.Services;
using ECommerceCMS.Data.Entity;
using AutoMapper;
using ECommerceCMS.Service;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using ECommerceCMS.Extensions;

namespace ECommerceCMS
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
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SocialAuth")));

            services.AddCors(option =>
            {
                option.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });

            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                   ValidateAudience=true,
                   ValidateIssuer=true,
                    ValidateLifetime = true,
                   ValidIssuer="Antasya",
                   ValidAudience="Antasya",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS Api", Version = "v1" });
            });
            
            services.AddElasticsearch(Configuration);

            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderCartItemRepository, OrderCartItemRepository>();
            services.AddScoped<IOrderCartRepository, OrderCartRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderCartService, OrderCartService>();
            services.AddScoped<IOrderCartItemService, OrderCartItemService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IBundleRepository, BundleRepository>();
            services.AddScoped<IBundleService, BundleService>();
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IListingRepository, ListingRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductCommentRepository, ProductCommentRepository>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuService, MenuService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS Api V1");
                    c.RoutePrefix = "swagger";
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseRouting();

            app.UseCors("EnableCors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                ///Configure the timeout to 5 minutes to avoid "The Angular CLI process did not start listening for requests within the timeout period of 50 seconds." issue
                spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            loggerFactory.AddLog4Net();

        }
    }
}

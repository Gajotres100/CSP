using System;
using System.Text;
using ComProvis.AV.AV;
using ComProvis.AV.Code;
using ComProvis.AV.Core;
using ComProvis.AV.Data;
using ComProvis.AV.Integration;
using ComProvis.AV.Params;
using ComProvis.AV.Services;
using ComProvis.AV.UI.Providers;
using ComProvis.AV.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace ComProvis.AV.UI
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
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AV", Version = "v1" });
            });

            services.AddDbContext<ComProvisAvDbContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ComProvisAvDbContext")));

            InitializeContainer(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "ComProvis.AV.UI",
                            ValidAudience = "http://localhost:51165/",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kjaksfjashfjashfjashjfashu"))
                        };
                    });

            services.AddMemoryCache();
            services.AddCors();

            services.AddMvc()
              .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromDays(1);
            });

            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(options => {
                options.WithOrigins(Configuration["CorsAllowedOriginFront"]);
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["VirtualDirectory"] + "/swagger/v1/swagger.json", "AV API V1");
            });

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc();

            app.UseStatusCodePages();
        }

        private void InitializeContainer(IServiceCollection services)
        {
            services.AddScoped<IComProvisAvDbContext, ComProvisAvDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IUiServices, UiServices>();
            services.AddScoped<ILicencesVM, LicencesVM>();
            services.AddScoped<IUserVM, UserVM>();
            services.AddScoped<IAvOperation, AvOperation>();
            services.AddScoped<IAvAppSettings, AvAppSettings>();
            services.AddScoped<IClient, Client>();
            services.AddScoped<ITrendMicroManager, TrendMicroManager>();
            services.AddScoped<IComponentProSamlProvider, ComponentProSamlProvider>();
        }
    }
}

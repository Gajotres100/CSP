using System.ServiceModel;
using ComProvis.AV.AV;
using ComProvis.AV.Code;
using ComProvis.AV.Core;
using ComProvis.AV.Data;
using ComProvis.AV.Integration;
using ComProvis.AV.Params;
using ComProvis.AV.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SaaSApi;
using SoapCore;

namespace ComProvis.AV
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);

            services.AddDbContext<ComProvisAvDbContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ComProvisAvDbContext")));

            InitializeContainer(services);

            services.AddMemoryCache();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSoapEndpoint<ComProvisAvSoap>("/SaaSApi", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
        }

        private void InitializeContainer(IServiceCollection services)
        {
            services.AddScoped<IComProvisAvDbContext, ComProvisAvDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IAvOperation, AvOperation>();
            services.AddScoped<IAvAppSettings, AvAppSettings>();
            services.AddScoped<IClient, Client>();
            services.AddScoped<ITrendMicroManager, TrendMicroManager>();
            services.TryAddScoped<ComProvisAvSoap>();
        }
    }
}

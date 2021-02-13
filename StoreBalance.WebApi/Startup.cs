using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using StoreBalance.WebApi.Application;
using StoreBalance.WebApi.Application.BackgroundJobs;
using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Repos;
using StoreBalance.WebApi.Domain.Services;
using StoreBalance.WebApi.Infrastructure.DatabaseSql;
using System;

namespace StoreBalance.WebApi
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

            #region Swagger
            services.AddOpenApiDocument(settings =>
            {
                settings.Title = "Store Balance";
            });
            #endregion

            #region Entity Framework
            services.AddDbContext<StoreBalanceContext>(opt =>
            {
                opt.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"), opts =>
                    {
                        opts.CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds);
                    });
            });
            #endregion

            #region Services
            services.AddScoped<IQueryBalanceService, QueryBalanceService>();
            services.AddScoped<IQueryWalletRecordsService, QueryWalletRecordsService>();
            services.AddScoped<ICreateNewWalletDebitService, CreateNewWalletDebitService>();
            services.AddScoped<ICreateNewWalletCreditService, CreateNewWalletCreditService>();
            services.AddScoped<ICreateNewWalletCreditFutureService, CreateNewWalletCreditFutureService>();
            services.AddScoped<IApplyFutureCreditService, ApplyFutureCreditService>();
            services.AddScoped<IQueryFutureBalanceService, QueryFutureBalanceService>();
            #endregion

            #region HostedServices
            services.AddHostedService<ApplyCreditHostedService>();
            #endregion

            #region Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}

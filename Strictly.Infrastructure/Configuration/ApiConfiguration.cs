using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strictly.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strictly.Infrastructure.Repository;
using Strictly.Application.Streaks;
using Strictly.Application.Users;
using Strictly.Application.Shared;
using Strictly.Application.CheckIns;
using Strictly.Application.Reminders;
using Strictly.Infrastructure.Configuration.AppSettings;
using Strictly.Application.Notifications;
using Strictly.Infrastructure.NotificationProviders;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Strictly.Infrastructure.Configuration
{
    public static class ApiConfiguration
    {
        /// <summary>
        /// Helper method to configure all services, providers, utilities, etc
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddStrictlyToApi(
            this WebApplicationBuilder hostApplicationBuilder)
        {
            hostApplicationBuilder.Services.BindAppSettings(hostApplicationBuilder.Configuration);
            hostApplicationBuilder.Services.AddDatabaseContext(hostApplicationBuilder.Configuration);
            hostApplicationBuilder.Services.AddServices();
            hostApplicationBuilder.Services.AddProviders();
            hostApplicationBuilder.AddLoggingAndMonitoring();
            return hostApplicationBuilder;
        }
        
        /// <summary>
        /// Add EF Core DB Context usin a connection string from config
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        /// <summary>
        /// Add Custom Services to DI Container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStreakRepo, StreakRepo>();
            services.AddScoped<IStreakService, StreakService>();
            services.AddScoped<ICheckInRepo, CheckInRepo>();
            services.AddScoped<ICheckInService, CheckInService>();
            services.AddScoped<IReminderRepo, ReminderRepo>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }

        /// <summary>
        /// Configure Providers like automapper to DI Container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddProviders(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<INotificationProviderFactory, NotificationProviderFactory>();
            services.AddScoped<MailkitProvider>();
            return services;
        }
        
        /// <summary>
        /// Configure logging and monitoring
        /// </summary>
        /// <returns></returns>
        private static WebApplicationBuilder AddLoggingAndMonitoring(
            this WebApplicationBuilder hostApplicationBuilder)
        {
            hostApplicationBuilder.Host.UseSerilog((hostingContext, loggerConfig) => loggerConfig.ReadFrom.Configuration(hostingContext.Configuration));
            return hostApplicationBuilder;
        }

        /// <summary>
        /// Configure AppSettings to concrete classes
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection BindAppSettings(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EmailSettings>()
                .Bind(configuration.GetSection("EmailSettings"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
        
    }
}

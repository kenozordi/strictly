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

namespace Strictly.Infrastructure.Configuration
{
    public static class WorkerConfiguration
    {
        /// <summary>
        /// Helper method to configure all services, providers, utilities, etc
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddStrictlyToWorker(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.BindAppSettings(configuration);
            services.AddDatabaseContext(configuration);
            services.AddServices();
            services.AddProviders();
            return services;
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
            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IStreakRepo, StreakRepo>();
            services.AddSingleton<IStreakService, StreakService>();
            services.AddSingleton<ICheckInRepo, CheckInRepo>();
            services.AddSingleton<ICheckInService, CheckInService>();
            services.AddSingleton<IReminderRepo, ReminderRepo>();
            services.AddSingleton<IReminderService, ReminderService>();
            services.AddSingleton<INotificationService, NotificationService>();

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
            services.AddSingleton<INotificationProviderFactory, NotificationProviderFactory>();
            services.AddSingleton<MailkitProvider>();
            return services;
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

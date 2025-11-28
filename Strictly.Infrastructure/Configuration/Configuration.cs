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
using AutoMapper;

namespace Strictly.Infrastructure.Configuration
{
    public static class Configuration
    {
        /// <summary>
        /// Add EF Core DB Context usin a connection string from config
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        /// <summary>
        /// Add Custom Services to DI Container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStreakRepo, StreakRepo>();
            services.AddScoped<IStreakService, StreakService>();

            return services;
        }

        /// <summary>
        /// Configure Providers like automapper to DI Container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddProviders(
            this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Strictly.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strictly.Application.Interfaces;
using Strictly.Infrastructure.Repository;
using Strictly.Application.Services;

namespace Strictly.Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

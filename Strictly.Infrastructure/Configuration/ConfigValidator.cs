using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Strictly.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration validations that happen after the services have been loaded into the DI container
    /// </summary>
    public static class ConfigValidator
    {
        public static void ValidateConfiguration(
            this IServiceProvider serviceProvider)
        {
            // Validate auto mapper
            var mapper = serviceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}

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
    public static class ConfigValidator
    {
        public static void ValidateConfiguration(
            this IServiceProvider serviceProvider)
        {
            var mapper = serviceProvider.GetRequiredService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}

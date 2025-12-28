using Microsoft.Extensions.DependencyInjection;
using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks.StreakFrequencies
{
    public class StreakFrequencyFactory : IStreakFrequencyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public StreakFrequencyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IStreakFrequency GetStreakFrequencyService(StreakFrequency streakFrequency)
        {
            switch (streakFrequency)
            {
                case StreakFrequency.Daily:
                    return _serviceProvider.GetRequiredService<DailyStreakService>();
                case StreakFrequency.Weekly:
                    throw new NotImplementedException();
                case StreakFrequency.Monthly:
                    return _serviceProvider.GetRequiredService<MonthlyStreakService>();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

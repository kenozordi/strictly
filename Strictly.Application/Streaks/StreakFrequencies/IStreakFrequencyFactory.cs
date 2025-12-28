using Strictly.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks.StreakFrequencies
{
    public interface IStreakFrequencyFactory
    {
        IStreakFrequency GetStreakFrequencyService(StreakFrequency streakFrequency);
    }
}

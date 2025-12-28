using Microsoft.Extensions.Logging;
using Strictly.Application.CheckIns;
using Strictly.Domain.Enum;
using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Application.Streaks.StreakFrequencies
{
    public class MonthlyStreakService : IStreakFrequency
    {
        protected readonly ICheckInRepo _checkInRepo;
        protected readonly ILogger<MonthlyStreakService> _logger;

        public MonthlyStreakService(ICheckInRepo checkInRepo, ILogger<MonthlyStreakService> logger)
        {
            _checkInRepo = checkInRepo;
            _logger = logger;

        }
        public async Task<DateTime> GetNextCheckInDate(Streak streak, DateTime date)
        {
            var now = DateTime.Now;
            var addOneMonth = now.Day > streak.EndDate.Day ? true : false;
            var firstCheckInDate = new DateTime(
                now.Year, 
                now.Month, 
                streak.EndDate.Day, 
                streak.EndDate.Hour, streak.EndDate.Minute, streak.EndDate.Second);

            return addOneMonth ? firstCheckInDate.AddMonths(1) : firstCheckInDate;
        }

        public async Task<(bool isSuccess, string message)> AddCheckInSchedule(Streak streak, DateTime firstCheckInDate)
        {
            // Todo: Wrap this in a DB transaction
            try
            {
                int noOfCheckinCreated = 0;
                DateTime today = DateTime.Now;
                bool addCurrentMonth = today.Day <= firstCheckInDate.Day
                    && today.TimeOfDay <= firstCheckInDate.TimeOfDay.Subtract(TimeSpan.FromMinutes(5))
                    ? true
                    : false;
                int numOfCheckins = addCurrentMonth ? 1 : 0 + ((streak.EndDate.Year - firstCheckInDate.Year) * 12) + streak.EndDate.Month - firstCheckInDate.Month;
                var dueDatePlaceholder = firstCheckInDate;
                for (int i = 0; i < numOfCheckins; i++)
                {
                    var checkIn = new CheckIn()
                    {
                        DueDate = dueDatePlaceholder,
                        StreakId = streak.Id,
                        UserId = streak.UserId,
                        Status = (int)CheckInStatus.Pending
                    };
                    noOfCheckinCreated += await _checkInRepo.CreateCheckIn(checkIn);

                    dueDatePlaceholder = dueDatePlaceholder.AddMonths(1);
                }

                return (true, noOfCheckinCreated.ToString());
            }
            catch (Exception ex)
            {
                return (false, "Something went wrong, An Exception Occured");
            }
        }

    }
}

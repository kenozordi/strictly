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
    public class DailyStreakService : IStreakFrequency
    {
        protected readonly ICheckInRepo _checkInRepo;
        protected readonly ILogger<DailyStreakService> _logger;

        public DailyStreakService(ICheckInRepo checkInRepo, ILogger<DailyStreakService> logger)
        {
            _checkInRepo = checkInRepo;
            _logger = logger;
            
        }

        public async Task<DateTime> GetNextCheckInDate(Streak streak, DateTime date)
        {
            var now = DateTime.Now;
            var addADay = now.TimeOfDay > streak.EndDate.TimeOfDay ? true : false;
            var firstCheckInDate = new DateTime(
                now.Year, 
                now.Month, 
                now.Day, 
                streak.EndDate.Hour, streak.EndDate.Minute, streak.EndDate.Second);

            return addADay ? firstCheckInDate.AddDays(1) : firstCheckInDate;
        }

        public async Task<(bool isSuccess, string message)> AddCheckInSchedule(Streak streak, DateTime firstCheckInDate)
        {
            // Todo: Wrap this in a DB transaction
            try
            {
                int noOfCheckinCreated = 0;
                DateTime today = DateTime.Now;
                bool addCurrentDay = today.Day <= firstCheckInDate.Day
                    && today.TimeOfDay <= firstCheckInDate.TimeOfDay.Subtract(TimeSpan.FromMinutes(5))
                    ? true
                    : false;
                int numOfCheckins = (streak.EndDate.Date - firstCheckInDate.Date).Days;
                numOfCheckins = addCurrentDay ? numOfCheckins += 1 : numOfCheckins;
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

                    dueDatePlaceholder = dueDatePlaceholder.AddDays(1);
                }

                return (true, noOfCheckinCreated.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Eceptio occured trying to AddDailyCheckInSchedule");
                return (false, "Something went wrong, An Exception Occured");
            }
        }

    }
}

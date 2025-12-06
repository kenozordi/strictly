using Microsoft.EntityFrameworkCore;
using Strictly.Application.CheckIns;
using Strictly.Domain.Models.CheckIns;
using Strictly.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Repository
{
    public class CheckInRepo : ICheckInRepo
    {
        protected AppDbContext _dbContext;

        public CheckInRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CheckIn>> GetActiveCheckInSchedule(Guid streakId)
        {
            return await _dbContext.CheckIns
                .AsNoTracking()
                .Where(s => s.StreakId == streakId && s.IsActive).ToListAsync();
        }

        public async Task<int> CreateCheckIn(CheckIn checkIn)
        {
            await _dbContext.CheckIns
                .AddAsync(checkIn);
            return await _dbContext.SaveChangesAsync();
        }
        
        public async Task<int> UpdateCheckIn(CheckIn checkIn)
        {
            _dbContext.CheckIns
                .Update(checkIn);
            return await _dbContext.SaveChangesAsync();
        }
        
        public async Task<CheckIn?> GetCheckIn(Guid checkInId)
        {
            return await _dbContext.CheckIns
                .FindAsync(checkInId);
        }
        
        public async Task<List<CheckIn>> GetCheckInForDate(Guid userId, DateTime date)
        {
            return await _dbContext.CheckIns.Where(c 
                => c.UserId == userId
                && c.DueDate.Date == date.Date)
                .Include(c => c.Streak)
                .ToListAsync();
        }

    }
}

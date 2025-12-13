using Microsoft.EntityFrameworkCore;
using Strictly.Application.Reminders;
using Strictly.Domain.Models.Reminders;
using Strictly.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Repository
{
    public class ReminderRepo : IReminderRepo
    {
        protected AppDbContext _dbContext;

        public ReminderRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public async Task<int> CreateReminder(Reminder reminder)
        {
            await _dbContext.Reminder
                .AddAsync(reminder);
            return await _dbContext.SaveChangesAsync();
        }
        
        public async Task<Reminder?> GetReminder(Guid reminderId)
        {
            return await _dbContext.Reminder
                .Where(r => r.Id == reminderId)
                .Include(r => r.Streak)
                .Include(r => r.User)
                .FirstOrDefaultAsync();
        }

    }
}

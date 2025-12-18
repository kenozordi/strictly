using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Strictly.Application.Reminders;
using Strictly.Domain.Enum;
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
        protected IDbContextFactory<AppDbContext> _dbContextFactory;

        public ReminderRepo(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> CreateReminder(Reminder reminder)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            await _dbContext.Reminder
                .AddAsync(reminder);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Reminder?> GetReminder(Guid reminderId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Reminder
                .Where(r => r.Id == reminderId)
                .Include(r => r.Streak)
                .Include(r => r.User)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Reminder>> GetActiveReminders(ReminderFrequency reminderFrequency)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Reminder
                .Where(r => r.IsActive)
                .Where(r => r.Streak.EndDate >= DateTime.Today)
                .Where(r => r.Frequency == reminderFrequency)
                .Where(r => r.UpdatedAt == default || r.UpdatedAt < DateTime.Today)
                .Include(r => r.User)
                .Include(r => r.Streak)
                .ToListAsync();
        }

        public async Task<int> UpdateReminder(Reminder reminder)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            _dbContext.Reminder
                .Update(reminder);
            return await _dbContext.SaveChangesAsync();
        }

    }
}

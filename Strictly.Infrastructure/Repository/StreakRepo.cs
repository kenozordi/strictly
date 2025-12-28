using Microsoft.EntityFrameworkCore;
using Strictly.Application.Streaks;
using Strictly.Domain.Models.StreakParticipants;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Users;
using Strictly.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Repository
{
    public class StreakRepo : IStreakRepo
    {
        protected IDbContextFactory<AppDbContext> _dbContextFactory;

        public StreakRepo(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> AddStreakParticipant(StreakParticipant streakParticipant)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            await _dbContext.StreakParticipant
                .AddAsync(streakParticipant);
            return await _dbContext.SaveChangesAsync();
        }
        
        public async Task<int> CreateStreak(Streak streak)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            await _dbContext.Streaks
                .AddAsync(streak);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Streak?> GetStreak(Guid streakId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Streaks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == streakId);
        }
        
        public async Task<StreakParticipant?> GetStreakParticipant(Guid streakId, Guid userId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.StreakParticipant
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StreakId == streakId && s.UserId == userId);
        }
        
        public async Task<List<StreakParticipant>> GetStreakParticipationByUserIdAsync(Guid userId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.StreakParticipant
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .Include(s => s.Streak)
                .ToListAsync();
        }

        public async Task<List<Streak>> GetStreakCreatedByUserIdAsync(Guid userId)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Streaks
                .AsNoTracking()
                .Where(s => s.UserId == userId).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Strictly.Application.Streaks;
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
        private readonly AppDbContext _dbContext;

        public StreakRepo(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();
        }

        public async Task<int> CreateStreak(Streak streak)
        {
            await _dbContext.Streaks
                .AddAsync(streak);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Streak?> GetStreak(Guid streakId)
        {
            return await _dbContext.Streaks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == streakId);
        }

        public async Task<List<Streak>> GetStreakByUserIdAsync(Guid userId)
        {
            return await _dbContext.Streaks
                .AsNoTracking()
                .Where(s => s.UserId == userId).ToListAsync();
        }
    }
}

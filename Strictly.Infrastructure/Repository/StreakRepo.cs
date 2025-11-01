using Microsoft.EntityFrameworkCore;
using Strictly.Application.Streaks;
using Strictly.Domain.Models.Entities;
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
        protected AppDbContext _dbContext;

        public StreakRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public async Task CreateStreak(Streak streak)
        {
            await _dbContext.Streaks.AddAsync(streak);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Streak>> GetStreakByUserIdAsync(Guid userId)
        {
            return await _dbContext.Streaks.Where(s => s.UserId == userId).ToListAsync();
        }
    }
}

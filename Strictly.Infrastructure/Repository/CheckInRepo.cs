using Microsoft.EntityFrameworkCore;
using Strictly.Application.CheckIns;
using Strictly.Domain.Models.CheckIns;
using Strictly.Domain.Models.Users;
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

        public async Task<List<CheckIn>> GetCheckInHistory(Guid streakId)
        {
            return await _dbContext.CheckIns
                .AsNoTracking()
                .Where(s => s.StreakId == streakId).ToListAsync();
        }

        //public async Task<List<CheckIn>> GetCheckInHistory(Guid streakId, Guid UserId)
        //{
        //    return await _dbContext.CheckIns
        //        .AsNoTracking()
        //        .Where(s => s.StreakId == streakId && s.userID).ToListAsync();
        //}
    }
}

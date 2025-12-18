using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Strictly.Application.Users;
using Strictly.Domain.Models.Users;
using Strictly.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Repository
{
    public class UserRepo : IUserRepo
    {
        protected IDbContextFactory<AppDbContext> _dbContextFactory;

        public UserRepo(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetUserAsync(Guid userGuid)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userGuid);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Strictly.Application.Interfaces;
using Strictly.Domain.Models.Entities;
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
        protected AppDbContext _dbContext;

        public UserRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;   
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .Include(u => u.Streaks)
                .ToListAsync();
        }
    }
}

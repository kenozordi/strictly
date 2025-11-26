using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Strictly.Domain.Models.Shared.Enum;
using Strictly.Domain.Models.Streaks;
using Strictly.Domain.Models.Users;
using Strictly.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.Seeders
{
    public static class UserDbSeeder
    {
        public static async Task SeedAsync(AppDbContext appDbContext)
        {
            await appDbContext.Database.MigrateAsync();
            var hasher = new PasswordHasher<User>();

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "kennyozordi@gmail.com",
            };
            user.PasswordHash = hasher.HashPassword(user, "Password123#");

            if (!await appDbContext.Users.AnyAsync())
            {
                await appDbContext.Users.AddAsync(user);
                await appDbContext.SaveChangesAsync();
            }
            
            if (!await appDbContext.Streaks.AnyAsync())
            {
                var streak = new Streak()
                {
                    Id = Guid.NewGuid(),
                    Title = "Reading a book",
                    Description = "Read the Art of War",
                    Frequency = StreakFrequency.Weekly,
                    UserId = user.Id
                };

                await appDbContext.Streaks.AddAsync(streak);
                await appDbContext.SaveChangesAsync();
            }

        }
    }
}

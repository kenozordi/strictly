﻿using Microsoft.EntityFrameworkCore;
using Strictly.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strictly.Infrastructure.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Streak> Streaks => Set<Streak>();
        public DbSet<CheckIn> CheckIns => Set<CheckIn>();
        public DbSet<Tag> Tags => Set<Tag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enum conversion
            modelBuilder.Entity<Streak>()
                .Property(s => s.Frequency)
                .HasConversion<string>();

            // Many-to-many Streak <-> Tag
            modelBuilder.Entity<Streak>()
                .HasMany(s => s.Tags)
                .WithMany(t => t.Streaks);

            // User -> Streak (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Streaks)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            // Streak -> CheckIn (1:N)
            modelBuilder.Entity<Streak>()
                .HasMany(s => s.CheckIns)
                .WithOne(c => c.Streak)
                .HasForeignKey(c => c.StreakId);

            // User -> Notification (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Streaks)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

        }
    }

}

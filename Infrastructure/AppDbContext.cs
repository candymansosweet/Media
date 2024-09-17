﻿using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<MediaFile> MediaFiles { get; set; }
        // ghi đè để trước khi lưu data, cập nhật ngày tháng
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSaving() // function your's define
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
            DateTime utcNow = DateTime.UtcNow;

            foreach (EntityEntry entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        // Set UpdatedDate to current date/time for updated entities
                        entry.Property("UpdatedDate").CurrentValue = utcNow;
                        break;
                    case EntityState.Added:
                        // Set CreatedDate and UpdatedDate to current date/time for new entities
                        entry.Property("CreatedDate").CurrentValue = utcNow;
                        entry.Property("UpdatedDate").CurrentValue = utcNow;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder
            .UseSqlServer("Data Source=.;Initial Catalog=Media;Integrated Security=True;TrustServerCertificate=True");
        return new AppDbContext(optionsBuilder.Options);
    }
}
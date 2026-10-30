using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Text;
using CoreModule.Domain.PostAgg;
using CoreModule.Infrastructure.Persistent.EF.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.EF
{
    public class CoreModuleCtx : DbContext
    {
        public CoreModuleCtx(DbContextOptions<CoreModuleCtx> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreModuleCtx).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

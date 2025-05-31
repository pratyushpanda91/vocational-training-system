using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using VocationalTrainingSystem.Models;
namespace VocationalTrainingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Approver> Approvers { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<PresentAddress> PresentAddresses { get; set; }
        public DbSet<PermanentAddress> PermanentAddresses { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Approver>().ToTable("Approvers");
        }
    }

}

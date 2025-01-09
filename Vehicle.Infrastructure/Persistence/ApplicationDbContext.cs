using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobAdvertiser> JobAdvertisers { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<JobSeekerSkill> JobSeekerSkills { get; set; }
        public DbSet<JobSeekerExperience> JobSeekerExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Configure column lengths
            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            // JobSeeker indexes
            modelBuilder.Entity<JobSeeker>()
                .HasIndex(js => js.Email)
                .IsUnique();
            modelBuilder.Entity<JobSeeker>()
                .Property(js => js.PhoneNumber)
                .HasMaxLength(15).IsRequired();

            // JobAdvertiser indexes
            modelBuilder.Entity<JobAdvertiser>()
            .HasIndex(ja => ja.ContactEmail)
            .IsUnique();
            modelBuilder.Entity<JobAdvertiser>()
                .Property(ja => ja.ContactPhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            // Specify relationships
            modelBuilder.Entity<JobSeekerSkill>()
                        .HasOne(js => js.JobSeeker)
                        .WithMany(j => j.Skills)
                        .HasForeignKey(js => js.JobSeekerId);

            modelBuilder.Entity<JobSeekerExperience>()
                     .HasOne(js => js.JobSeeker)
                     .WithMany(j => j.Experience)
                     .HasForeignKey(js => js.JobSeekerId);
        }
    }
}

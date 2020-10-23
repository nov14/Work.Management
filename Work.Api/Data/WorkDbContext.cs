using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;
using Work.Api.Models;

namespace Work.Api.Data
{
    public class WorkDbContext: DbContext
    {
        public WorkDbContext(DbContextOptions<WorkDbContext> options): base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TokenModel> TokenModels { get; set; }
        public DbSet<LoginRole> LoginRoles { get; set; }
        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Company>()
                .Property(x => x.Introduction)
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(x => x.TokenModel)
                .WithOne(x => x.User)
                .HasForeignKey<TokenModel>(x => x.UserId);

            modelBuilder.Entity<TokenModel>().HasKey(x => x.Uid);

            modelBuilder.Entity<Url>()
                .HasOne(x => x.LoginRole)
                .WithMany(x => x.Urls)
                .HasForeignKey(x => x.RoleId);

        }
    }
}

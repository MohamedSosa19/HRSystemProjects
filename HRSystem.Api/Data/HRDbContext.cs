using HRSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Data
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee Configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");

                // Relationship with Department
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Department Configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Description).HasMaxLength(500);

                // Self-referencing relationship for Manager
                entity.HasOne(d => d.Manager)
                    .WithMany()
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // LeaveType Configuration
            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.HasKey(lt => lt.Id);
                entity.Property(lt => lt.Name).IsRequired().HasMaxLength(50);
                entity.Property(lt => lt.Description).HasMaxLength(200);
            });

            // LeaveRequest Configuration
            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.HasKey(lr => lr.Id);
                entity.Property(lr => lr.Reason).HasMaxLength(500);
                entity.Property(lr => lr.ApprovalComments).HasMaxLength(200);

                // Relationships
                entity.HasOne(lr => lr.Employee)
                    .WithMany(e => e.LeaveRequests)
                    .HasForeignKey(lr => lr.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict); // غيرناها من Cascade لـ Restrict

                entity.HasOne(lr => lr.LeaveType)
                    .WithMany(lt => lt.LeaveRequests)
                    .HasForeignKey(lr => lr.LeaveTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(lr => lr.ApprovedBy)
                    .WithMany()
                    .HasForeignKey(lr => lr.ApprovedById)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // LeaveBalance Configuration
            modelBuilder.Entity<LeaveBalance>(entity =>
            {
                entity.HasKey(lb => lb.Id);

                // Composite unique index
                entity.HasIndex(lb => new { lb.EmployeeId, lb.LeaveTypeId, lb.Year })
                    .IsUnique();

                // Relationships
                entity.HasOne(lb => lb.Employee)
                    .WithMany(e => e.LeaveBalances)
                    .HasForeignKey(lb => lb.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict); // غيرناها من Cascade لـ Restrict

                entity.HasOne(lb => lb.LeaveType)
                    .WithMany(lt => lt.LeaveBalances)
                    .HasForeignKey(lb => lb.LeaveTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

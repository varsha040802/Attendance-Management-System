using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AttandanceManagementSystem.Models;

public partial class AMSDbContext : DbContext
{
    public AMSDbContext()
    {
    }

    public AMSDbContext(DbContextOptions<AMSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261C76894C9B");

            entity.HasOne(d => d.User).WithMany(p => p.Attendances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Attendance");
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD603E0276A8AE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE6C8A399");

            entity.HasOne(d => d.Designation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Designati__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

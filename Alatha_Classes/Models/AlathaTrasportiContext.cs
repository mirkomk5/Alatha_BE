using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Alatha_Classes.Models;

public partial class AlathaTrasportiContext : DbContext
{
    public AlathaTrasportiContext()
    {
    }

    public AlathaTrasportiContext(DbContextOptions<AlathaTrasportiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StaffInfo> StaffInfos { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=dbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StaffInfo>(entity =>
        {
            entity.ToTable("StaffInfo");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ContractUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC07608D5D0A");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

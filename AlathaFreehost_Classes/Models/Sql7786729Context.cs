using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AlathaFreehost_Classes.Models;

public partial class Sql7786729Context : DbContext
{
    public Sql7786729Context()
    {
    }

    public Sql7786729Context(DbContextOptions<Sql7786729Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchiveInfo> ArchiveInfos { get; set; }

    public virtual DbSet<StaffInfo> StaffInfos { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=sql7.freesqldatabase.com;database=sql7786729;user=sql7786729;password=NZjIXfzENF;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.62-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<ArchiveInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ArchiveInfo");

            entity.Property(e => e.Id)
                .UseCollation("utf8mb4_bin")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.DailyWorkHour).HasColumnType("int(10)");
        });

        modelBuilder.Entity<StaffInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("StaffInfo");

            entity.Property(e => e.Id)
                .UseCollation("utf8mb4_bin")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.ArchiveId)
                .UseCollation("utf8mb4_bin")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(3);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id)
                .UseCollation("utf8mb4_bin")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Role).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

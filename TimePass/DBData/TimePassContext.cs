using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimePass.DBData;

public partial class TimePassContext : DbContext
{
    public TimePassContext()
    {
    }

    public TimePassContext(DbContextOptions<TimePassContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acitvity> Acitvities { get; set; }

    public virtual DbSet<Adminlogin> Adminlogins { get; set; }

    public virtual DbSet<Oprator> Oprators { get; set; }

    public virtual DbSet<Para> Paras { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<RptReward> RptRewards { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=P3NWPLSK12SQL-v12.shr.prod.phx3.secureserver.net;Database=_TimePass;user id=_TimePass;password=TimePass@123@#; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("_TimePass");

        modelBuilder.Entity<Acitvity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Acitvity__3214EC27A2A30814");

            entity.ToTable("Acitvity");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Coins).HasColumnName("COINS");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PlayNumber).HasMaxLength(10);
            entity.Property(e => e.PlayTime).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Adminlogin>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__ADMINLOG__C9F284579BB983F8");

            entity.ToTable("ADMINLOGIN");

            entity.Property(e => e.UserName).HasMaxLength(20);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<Oprator>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__OPRATOR__C9F2845767AE029B");

            entity.ToTable("OPRATOR");

            entity.Property(e => e.UserName).HasMaxLength(20);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<Para>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PARA__3214EC27DFAD9AE7");

            entity.ToTable("PARA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Serveractive).HasColumnName("SERVERACTIVE");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC27BBFD198C");

            entity.ToTable("Payment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PlayTime).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__REGISTRA__1788CCAC0C560E46");

            entity.ToTable("REGISTRATION");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Coins).HasColumnName("COINS");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(25);
            entity.Property(e => e.Photo).HasMaxLength(250);
            entity.Property(e => e.Username).HasMaxLength(25);
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESULT__3214EC2736C224EA");

            entity.ToTable("RESULT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active)
                .HasMaxLength(5)
                .HasColumnName("ACTIVE");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PlayNumber).HasMaxLength(10);
            entity.Property(e => e.PlayTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<RptReward>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RptREWARD");

            entity.Property(e => e.Coin).HasColumnName("COIN");
            entity.Property(e => e.Playnumber)
                .HasMaxLength(10)
                .HasColumnName("PLAYNUMBER");
            entity.Property(e => e.Playtime)
                .HasColumnType("datetime")
                .HasColumnName("PLAYTIME");
            entity.Property(e => e.Userid).HasColumnName("USERID");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC276873657E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TTime)
                .HasColumnType("datetime")
                .HasColumnName("tTime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

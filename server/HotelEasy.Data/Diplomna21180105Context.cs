using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Entities;

namespace HotelEasy.Data;

public partial class Diplomna21180105Context : DbContext
{
    public Diplomna21180105Context()
    {
    }

    public Diplomna21180105Context(DbContextOptions<Diplomna21180105Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Log21180105> Log21180105s { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Diplomna21180105;User Id=sa;Password=${MSSQL_SA_PASSWORD};TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AI");

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotels__46023BBF1438359F");

            entity.ToTable("Hotels", "21180105", tb => tb.HasTrigger("trg_Hotels_Log"));

            entity.Property(e => e.HotelId).HasColumnName("HotelID");
            entity.Property(e => e.CreatedAt21180105)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt_21180105");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hotels_Users");
        });

        modelBuilder.Entity<Log21180105>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__log_2118__5E5499A825722449");

            entity.ToTable("log_21180105", "21180105");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.OperationDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OperationType).HasMaxLength(10);
            entity.Property(e => e.TableName).HasMaxLength(100);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F0445623A8F");

            entity.ToTable("Reservations", "21180105", tb => tb.HasTrigger("trg_Reservations_Log"));

            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.CreatedAt21180105)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt_21180105");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Rooms");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Users");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__3286391939AFB588");

            entity.ToTable("Rooms", "21180105", tb => tb.HasTrigger("trg_Rooms_Log"));

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.CreatedAt21180105)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt_21180105");
            entity.Property(e => e.HotelId).HasColumnName("HotelID");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomNumber).HasMaxLength(10);
            entity.Property(e => e.RoomType).HasMaxLength(50);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_Hotels");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD51D85C9");

            entity.ToTable("Users", "21180105", tb => tb.HasTrigger("trg_Users_Log"));

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534B738B1EE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt21180105)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt_21180105");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

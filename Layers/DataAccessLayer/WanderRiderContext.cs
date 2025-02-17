using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WanderRiderRentACar.DMO;

namespace WanderRiderRentACar.DataAccessLayer;

public partial class WanderRiderContext : DbContext
{
    public WanderRiderContext()
    {
    }

    public WanderRiderContext(DbContextOptions<WanderRiderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<FuelType> FuelTypes { get; set; }

    public virtual DbSet<RentStore> RentStores { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<Transmission> Transmissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0342E86FEF817");

            entity.Property(e => e.CarName).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            entity.HasOne(d => d.FuelType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.FuelTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__FuelTypeId__4222D4EF");

            entity.HasOne(d => d.RentStores).WithMany(p => p.Cars)
                .HasForeignKey(d => d.RentStoresId)
                .HasConstraintName("FK_Cars_RentStores");

            entity.HasOne(d => d.Segment).WithMany(p => p.Cars)
                .HasForeignKey(d => d.SegmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__SegmentId__440B1D61");

            entity.HasOne(d => d.Transmission).WithMany(p => p.Cars)
                .HasForeignKey(d => d.TransmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__Transmissi__4316F928");
        });

        modelBuilder.Entity<FuelType>(entity =>
        {
            entity.HasKey(e => e.FuelTypeId).HasName("PK__FuelType__048BEE3795F10966");

            entity.Property(e => e.FuelTypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<RentStore>(entity =>
        {
            entity.HasKey(e => e.RentStoresId).HasName("PK__RentStor__3903F197B084651A");

            entity.Property(e => e.RentStoresName).HasMaxLength(255);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK__Sales__C952FB32DA5E0FED");

            entity.HasOne(d => d.Car).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__CarId__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.Sales)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__UserId__46E78A0C");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.SegmentId).HasName("PK__Segments__C680677B1BC4EDB3");

            entity.Property(e => e.DepositPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SegmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<Transmission>(entity =>
        {
            entity.HasKey(e => e.TransmissionId).HasName("PK__Transmis__56E90A0E7EB896F5");

            entity.Property(e => e.TransmissionName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C2B1F40E7");

            entity.Property(e => e.LicenceNo).HasMaxLength(50);
            entity.Property(e => e.UserAddress).HasMaxLength(255);
            entity.Property(e => e.UserEmail).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.UserPhone).HasMaxLength(50);
            entity.Property(e => e.UserSurname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

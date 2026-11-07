using System;
using System.Collections.Generic;
using App.Domain.Core.AppFile.Entities;
using App.Domain.Core.Category.Entities;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.Service.Entities;
using App.Domain.Core.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace App.Infrastructure.DataBase.SQL.DbContext;

public partial class AppDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }


    public virtual DbSet<ExpertFavoritService> ExpertFavoritServices { get; set; }

    public virtual DbSet<AppFile> Files { get; set; }
    public virtual DbSet<FileType> FileTypes { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderFile> OrderFiles { get; set; }

    public virtual DbSet<OrederProposal> OredeProposals { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceFile> ServiceFiles { get; set; }
    public virtual DbSet<Expert> Experts { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Admin>()
       .HasOne(a => a.User)
       .WithOne()
       .HasForeignKey<Admin>(a => a.UserId);

        modelBuilder.Entity<Admin>()
            .HasIndex(a => a.UserId)
            .IsUnique();


        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.Bids)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bids_Orders");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsFixedLength();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Discription)
                .HasMaxLength(1000)
                .IsFixedLength();

            entity.HasOne(d => d.Order).WithMany(p => p.Comments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Orders");
        });

       

        modelBuilder.Entity<ExpertFavoritService>(entity =>
        {
            entity.ToTable("ExpertFavoritService");

            entity.HasOne(d => d.Service).WithMany(p => p.ExpertFavoritServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExpertFavoritService_Services");
        });

        modelBuilder.Entity<AppFile >(entity =>
        {
            entity.Property(e => e.Path)
                .HasMaxLength(250)
                .IsFixedLength();
        });
        modelBuilder.Entity<FileType>(entity =>
        {
            entity.HasMany(d => d.AppFiles).WithOne(p => p.FileType)
                .HasForeignKey(d => d.FileTypeId)
                .OnDelete(DeleteBehavior.NoAction);
                
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Discription)
                .HasMaxLength(1000)
                .IsFixedLength();

            entity.HasOne(d => d.Service).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Services");
            entity.HasOne(o => o.Expert)
    .WithMany()
    .HasForeignKey(o => o.ExpertId)
    .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<OrderFile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.File).WithMany(p => p.OrderFiles)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderFiles_Files");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderFiles)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderFiles_Orders");
        });

        modelBuilder.Entity<OrederProposal>(entity =>
        {
            entity.ToTable("OredeProposal");

            entity.Property(e => e.Discription)
                .HasMaxLength(1000)
                .IsFixedLength();

            entity.HasOne(d => d.Order).WithMany(p => p.OredeProposals)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OredeProposal_Orders");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Disctiption)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_Categories");
        });

        modelBuilder.Entity<ServiceFile>(entity =>
        {
            entity.HasOne(d => d.File).WithMany(p => p.ServiceFiles)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceFiles_Files");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceFiles)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceFiles_Services");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

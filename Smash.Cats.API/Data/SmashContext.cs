﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Smash.Cats.API.Models;

namespace Smash.Cats.API.Data
{
    public partial class SmashContext : DbContext
    {
        public SmashContext()
        {
        }

        public SmashContext(DbContextOptions<SmashContext> options)
            : base(options)
        {
        }

       // public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<Achievement> Achievements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=178.89.186.221, 1434;initial catalog=turganbaev_db;user id=turganbaev_user;password=e&w0Q41k4;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("turganbaev_user");

           /* modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Achievements)
                      .WithOne(a => a.User)
                      .HasForeignKey(a => a.UserId);
            });*/
            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.DateTime).HasDefaultValueSql("(N'')");

                entity.Property(e => e.PathImg)
                    .HasColumnName("pathIMG")
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

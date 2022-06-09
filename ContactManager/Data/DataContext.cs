using System;
using System.Collections.Generic;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactManager.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressBook> AddressBooks { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<UserDetail> UserDetails { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=HYD-KPOTNURU\\MSSQLSERVER01;Database=ContactManager;integrated security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressBook>(entity =>
            {
                entity.HasKey(e => e.PkaddressId)
                    .HasName("PK__Address__03684F1D68DEBF99");

                entity.HasOne(d => d.Fkstate)
                    .WithMany(p => p.AddressBooks)
                    .HasForeignKey(d => d.FkstateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__FKState__5CD6CB2B");

                entity.HasOne(d => d.Fkuser)
                    .WithMany(p => p.AddressBooks)
                    .HasForeignKey(d => d.FkuserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__FKUserI__5DCAEF64");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.PkstateId)
                    .HasName("PK__State__E5E7F3B2588594BB");

                entity.HasOne(d => d.Fkcountry)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.FkcountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__State__FKCountry__239E4DCF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

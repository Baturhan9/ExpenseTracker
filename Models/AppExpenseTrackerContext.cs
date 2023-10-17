using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models;

public partial class AppExpenseTrackerContext : DbContext
{
    public AppExpenseTrackerContext()
    {
    }

    public AppExpenseTrackerContext(DbContextOptions<AppExpenseTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ExpenseGroup> ExpenseGroups { get; set; }

    public virtual DbSet<Spending> Spendings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=app_expense_tracker;username=postgres;password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CLogin)
                .HasMaxLength(50)
                .HasColumnName("c_login");
            entity.Property(e => e.CName)
                .HasMaxLength(50)
                .HasColumnName("c_name");
            entity.Property(e => e.CPassword)
                .HasMaxLength(50)
                .HasColumnName("c_password");
        });

        modelBuilder.Entity<ExpenseGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("expense_group_pkey");

            entity.ToTable("expense_group");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.EgAmountSpends)
                .HasPrecision(10, 2)
                .HasColumnName("eg_amount_spends");
            entity.Property(e => e.EgExtraInfo)
                .HasMaxLength(150)
                .HasColumnName("eg_extra_info");
            entity.Property(e => e.EgName)
                .HasMaxLength(50)
                .HasColumnName("eg_name");

            entity.HasOne(d => d.Client).WithMany(p => p.ExpenseGroups)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_client_id");
        });

        modelBuilder.Entity<Spending>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spending_pkey");

            entity.ToTable("spending");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ExpenseGroupId).HasColumnName("expense_group_id");
            entity.Property(e => e.SExtraInfo)
                .HasMaxLength(150)
                .HasColumnName("s_extra_info");
            entity.Property(e => e.SValue)
                .HasPrecision(10, 2)
                .HasColumnName("s_value");

            entity.HasOne(d => d.Client).WithMany(p => p.Spendings)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_client_id");

            entity.HasOne(d => d.ExpenseGroup).WithMany(p => p.Spendings)
                .HasForeignKey(d => d.ExpenseGroupId)
                .HasConstraintName("fk_expense_group_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

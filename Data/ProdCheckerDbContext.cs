using System;
using System.Collections.Generic;
using JigNetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JigNetApi.Data;

public partial class ProdCheckerDbContext : DbContext
{
    public ProdCheckerDbContext(DbContextOptions<ProdCheckerDbContext> options)
        : base(options) { }

    public virtual DbSet<T_IOT_MASTER> T_IOT_MASTERs { get; set; }

    public virtual DbSet<T_JIGNET_CHECKER> T_JIGNET_CHECKERs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("JIG").UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<T_IOT_MASTER>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("T_IOT_MASTER_PK");
        });

        modelBuilder.Entity<T_JIGNET_CHECKER>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("T_JIGNET_CHECKER_PK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

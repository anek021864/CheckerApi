using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JigNetApi.Data;

public partial class ProdCheckerPostgreSqlDqlDbContext : DbContext
{
    public ProdCheckerPostgreSqlDqlDbContext(DbContextOptions<ProdCheckerPostgreSqlDqlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<t_iot_master> t_iot_masters { get; set; }

    public virtual DbSet<t_jignet_checker> t_jignet_checkers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<t_iot_master>(entity =>
        {
            entity.HasKey(e => e.id).HasName("t_iot_master_pk");
        });

        modelBuilder.Entity<t_jignet_checker>(entity =>
        {
            entity.HasKey(e => e.id).HasName("t_jignet_checker_pk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

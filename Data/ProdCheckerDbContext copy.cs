// using System;
// using System.Collections.Generic;
// using JigNetApi.Models;
// using Microsoft.EntityFrameworkCore;

// namespace JigNetApi.Data;

// public partial class ProdCheckerDbContext : DbContext
// {
//     public ProdCheckerDbContext(DbContextOptions<ProdCheckerDbContext> options)
//         : base(options) { }

//     public virtual DbSet<T_IOT_MASTER> T_IOT_MASTERs { get; set; }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.HasDefaultSchema("JIG").UseCollation("USING_NLS_COMP");

//         modelBuilder.Entity<T_IOT_MASTER>(entity =>
//         {
//             entity.HasKey(e => e.ID).HasName("T_IOT_MASTER_PK");

//             entity.ToTable("T_IOT_MASTER");

//             entity.Property(e => e.ID).HasMaxLength(24).IsUnicode(false);
//             entity.Property(e => e.CHECKER_TYPE).HasColumnType("NUMBER(1)");
//             entity.Property(e => e.CHEKER_NAME).HasMaxLength(500).IsUnicode(false);
//             entity.Property(e => e.COMPUTER_NAME).HasMaxLength(11).IsUnicode(false);
//             entity.Property(e => e.COUNTCLEARFLG).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.DATA).HasColumnType("CLOB");
//             entity.Property(e => e.DATAINFO).HasMaxLength(50).IsUnicode(false);
//             entity.Property(e => e.DESTINATION).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.FAILCODE).HasMaxLength(5).IsUnicode(false);
//             entity.Property(e => e.FAILNAME).HasMaxLength(500).IsUnicode(false);
//             entity.Property(e => e.FINISH_TIME).HasMaxLength(10).IsUnicode(false);
//             entity.Property(e => e.INFO).IsUnicode(false);
//             entity.Property(e => e.LOCATION).HasColumnType("NUMBER(38)");
//             entity.Property(e => e.MODEL).HasMaxLength(20).IsUnicode(false);
//             entity.Property(e => e.NOTICECONDITIONFLG).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.ORDERNO).HasMaxLength(50).IsUnicode(false);
//             entity.Property(e => e.PRODUCTIONLINE).HasMaxLength(20).IsUnicode(false);
//             entity.Property(e => e.PRODUCTIONTIME).HasColumnType("DATE");
//             entity.Property(e => e.PRODUCTION_TYPE).HasColumnType("NUMBER(38)");
//             entity.Property(e => e.PRODUCT_SN).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.PROGRAM_VERSION).HasColumnType("FLOAT");
//             entity.Property(e => e.REF1_VALUE).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.REF2_VALUE).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.REF3_VALUE).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.REF4_VALUE).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.REF5_VALUE).HasMaxLength(100).IsUnicode(false);
//             entity.Property(e => e.RESULT).HasColumnType("NUMBER(38)");
//             entity.Property(e => e.ROMVERSION).HasMaxLength(20).IsUnicode(false);
//             entity.Property(e => e.SHIFT).HasColumnType("NUMBER(38)");
//             entity.Property(e => e.SPECHI).HasColumnType("CLOB");
//             entity.Property(e => e.SPECLOW).HasColumnType("CLOB");
//             entity.Property(e => e.START_TIME).HasMaxLength(10).IsUnicode(false);
//             entity.Property(e => e.STEP_TYPE).HasColumnType("NUMBER(38)");
//             entity.Property(e => e.STEP_VERSION).HasColumnType("FLOAT");
//             entity.Property(e => e.TACTTIME).HasMaxLength(50).IsUnicode(false);
//             entity.Property(e => e.TESTNAME).HasColumnType("CLOB");
//             entity.Property(e => e.TESTNO).HasColumnType("CLOB");
//             entity.Property(e => e.UNIT).IsUnicode(false);
//             entity.Property(e => e.USBSERIAL).HasMaxLength(50).IsUnicode(false);
//         });

//         OnModelCreatingPartial(modelBuilder);
//     }

//     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
// }

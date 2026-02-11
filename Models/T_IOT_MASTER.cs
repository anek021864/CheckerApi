using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JigNetApi.Models;

[Table("T_IOT_MASTER")]
public partial class T_IOT_MASTER
{
    [Key]
    [StringLength(24)]
    [Unicode(false)]
    public string ID { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? PRODUCT_SN { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? COMPUTER_NAME { get; set; }

    [Column(TypeName = "NUMBER(38)")]
    public decimal? LOCATION { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MODEL { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PRODUCTIONLINE { get; set; }

    [Column(TypeName = "NUMBER(38)")]
    public decimal? RESULT { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? FAILCODE { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? CHEKER_NAME { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? PRODUCTIONTIME { get; set; }

    [Column(TypeName = "NUMBER(38)")]
    public decimal? SHIFT { get; set; }

    [Column(TypeName = "NUMBER(1)")]
    public bool CHECKER_TYPE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? REF1_VALUE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? REF2_VALUE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? REF3_VALUE { get; set; }

    [Column(TypeName = "FLOAT")]
    public decimal? PROGRAM_VERSION { get; set; }

    [Column(TypeName = "NUMBER(38)")]
    public decimal? PRODUCTION_TYPE { get; set; }

    [Column(TypeName = "FLOAT")]
    public decimal? STEP_VERSION { get; set; }

    [Column(TypeName = "NUMBER(38)")]
    public decimal? STEP_TYPE { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? FAILNAME { get; set; }

    [Column(TypeName = "CLOB")]
    public string? DATA { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? START_TIME { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? FINISH_TIME { get; set; }

    [Column(TypeName = "CLOB")]
    public string? TESTNAME { get; set; }

    [Column(TypeName = "CLOB")]
    public string? TESTNO { get; set; }

    [Unicode(false)]
    public string? UNIT { get; set; }

    [Unicode(false)]
    public string? INFO { get; set; }

    [Column(TypeName = "CLOB")]
    public string? SPECLOW { get; set; }

    [Column(TypeName = "CLOB")]
    public string? SPECHI { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TACTTIME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? USBSERIAL { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ORDERNO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DESTINATION { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ROMVERSION { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? REF4_VALUE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? REF5_VALUE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NOTICECONDITIONFLG { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? COUNTCLEARFLG { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DATAINFO { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JigNetApi.Models;

[Table("T_JIGNET_CHECKER")]
[Index("PRODSERIALNO", "JIGNAME", Name = "I_JIGNET_JIGNAME")]
[Index("DATECHECK", "MODEL", "CELL", "JIGNAME", Name = "T_JIGNET_CHECKER_SCAN1")]
public partial class T_JIGNET_CHECKER
{
    [Key]
    [StringLength(255)]
    [Unicode(false)]
    public string ID { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? PRODSERIALNO { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? COMPUTERNAME { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? MODEL { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? CELL { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? RESULTPASSFAIL { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? FAILNUMBER { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? JIGNAME { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? DATECHECK { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? TIMEFINISH { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? WIRESSNO { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? PROGRAMVER { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? STEPLISTVER { get; set; }

    [Column(TypeName = "CLOB")]
    public string? ALLDATALOG { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? NGNAME { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? POSITION { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CHT { get; set; }

    [Unicode(false)]
    public string? TESTNAME { get; set; }

    [Unicode(false)]
    public string? TESTNO { get; set; }

    [Column(TypeName = "CLOB")]
    public string? SPECHI { get; set; }

    [Column(TypeName = "CLOB")]
    public string? SPECLOW { get; set; }

    [Column(TypeName = "CLOB")]
    public string? DATA { get; set; }
}

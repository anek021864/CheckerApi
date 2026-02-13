using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JigNetApi.Data;

[Table("t_jignet_checker", Schema = "JIGDWHTEST")]
[Index("datecheck", Name = "i_jignet_date")]
[Index("model", "cell", Name = "i_jignet_location")]
[Index("prodserialno", Name = "i_jignet_serno")]
public partial class t_jignet_checker
{
    [Key]
    [StringLength(255)]
    public string id { get; set; } = null!;

    [StringLength(255)]
    public string? prodserialno { get; set; }

    [StringLength(255)]
    public string? computername { get; set; }

    [StringLength(255)]
    public string? model { get; set; }

    [StringLength(255)]
    public string? cell { get; set; }

    [StringLength(255)]
    public string? resultpassfail { get; set; }

    [StringLength(255)]
    public string? failnumber { get; set; }

    [StringLength(255)]
    public string? jigname { get; set; }

    [StringLength(255)]
    public string? datecheck { get; set; }

    [StringLength(255)]
    public string? timefinish { get; set; }

    [StringLength(255)]
    public string? wiressno { get; set; }

    [StringLength(255)]
    public string? programver { get; set; }

    [StringLength(255)]
    public string? steplistver { get; set; }

    public string? alldatalog { get; set; }

    [StringLength(255)]
    public string? ngname { get; set; }

    [StringLength(255)]
    public string? position { get; set; }

    [StringLength(100)]
    public string? cht { get; set; }

    [StringLength(4000)]
    public string? testname { get; set; }

    [StringLength(4000)]
    public string? testno { get; set; }

    public string? spechi { get; set; }

    public string? speclow { get; set; }

    public string? data { get; set; }
}

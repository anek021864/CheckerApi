using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JigNetApi.Data;

[Table("t_iot_master", Schema = "JIGDWHTEST")]
[Index("productiontime", Name = "t_iot_master_index1", AllDescending = true)]
public partial class t_iot_master
{
    [Key]
    [StringLength(24)]
    public string id { get; set; } = null!;

    [StringLength(100)]
    public string? product_sn { get; set; }

    [StringLength(11)]
    public string? computer_name { get; set; }

    public decimal? location { get; set; }

    [StringLength(20)]
    public string? model { get; set; }

    [StringLength(20)]
    public string? productionline { get; set; }

    public decimal? result { get; set; }

    [StringLength(5)]
    public string? failcode { get; set; }

    [StringLength(500)]
    public string? cheker_name { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? productiontime { get; set; }

    public decimal? shift { get; set; }

    public bool checker_type { get; set; }

    [StringLength(100)]
    public string? ref1_value { get; set; }

    [StringLength(100)]
    public string? ref2_value { get; set; }

    [StringLength(100)]
    public string? ref3_value { get; set; }

    public decimal? program_version { get; set; }

    public decimal? production_type { get; set; }

    public decimal? step_version { get; set; }

    public decimal? step_type { get; set; }

    [StringLength(500)]
    public string? failname { get; set; }

    public string? data { get; set; }

    [StringLength(10)]
    public string? start_time { get; set; }

    [StringLength(10)]
    public string? finish_time { get; set; }

    public string? testname { get; set; }

    public string? testno { get; set; }

    [StringLength(4000)]
    public string? unit { get; set; }

    [StringLength(4000)]
    public string? info { get; set; }

    public string? speclow { get; set; }

    public string? spechi { get; set; }

    [StringLength(50)]
    public string? tacttime { get; set; }

    [StringLength(50)]
    public string? usbserial { get; set; }

    [StringLength(50)]
    public string? orderno { get; set; }

    [StringLength(100)]
    public string? destination { get; set; }

    [StringLength(20)]
    public string? romversion { get; set; }

    [StringLength(100)]
    public string? ref4_value { get; set; }

    [StringLength(100)]
    public string? ref5_value { get; set; }

    [StringLength(100)]
    public string? noticeconditionflg { get; set; }

    [StringLength(100)]
    public string? countclearflg { get; set; }

    [StringLength(50)]
    public string? datainfo { get; set; }
}

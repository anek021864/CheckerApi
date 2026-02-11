using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JigNetApi.Dtos;

public class TIotMasterCreateDto
{
    [JsonPropertyName("productSn")]
    [Required]
    [MaxLength(100)]
    public string? ProductSn { get; set; }

    [JsonPropertyName("computerName")]
    [Required]
    [MaxLength(11)]
    public string? ComputerName { get; set; }

    [JsonPropertyName("location")]
    public decimal? Location { get; set; }

    [JsonPropertyName("model")]
    [Required]
    [MaxLength(20)]
    public string? Model { get; set; }

    [JsonPropertyName("productionLine")]
    [Required]
    [MaxLength(20)]
    public string? ProductionLine { get; set; }

    [JsonPropertyName("result")]
    [Required]
    public decimal? Result { get; set; }

    [JsonPropertyName("failCode")]
    [MaxLength(5)]
    public string? FailCode { get; set; }

    [JsonPropertyName("checkerName")]
    [Required]
    [MaxLength(500)]
    public string? CheckerName { get; set; }

    [JsonPropertyName("productionTime")]
    [Required]
    public DateTime? ProductionTime { get; set; } = DateTime.Now;

    [JsonPropertyName("shift")]
    public decimal? Shift { get; set; }

    [JsonPropertyName("checkerType")]
    public bool CheckerType { get; set; }

    [JsonPropertyName("ref1Value")]
    public string? Ref1Value { get; set; }

    [JsonPropertyName("ref2Value")]
    public string? Ref2Value { get; set; }

    [JsonPropertyName("ref3Value")]
    public string? Ref3Value { get; set; }

    [JsonPropertyName("programVersion")]
    public decimal? ProgramVersion { get; set; }

    [JsonPropertyName("productionType")]
    public decimal? ProductionType { get; set; }

    [JsonPropertyName("stepVersion")]
    public decimal? StepVersion { get; set; }

    [JsonPropertyName("stepType")]
    public decimal? StepType { get; set; }

    [JsonPropertyName("failName")]
    [MaxLength(500)]
    public string? FailName { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("startTime")]
    [MaxLength(10)]
    public string? StartTime { get; set; }

    [JsonPropertyName("finishTime")]
    [MaxLength(10)]
    public string? FinishTime { get; set; }

    [JsonPropertyName("testName")]
    public string? TestName { get; set; }

    [JsonPropertyName("testNo")]
    public string? TestNo { get; set; }

    [JsonPropertyName("unit")]
    public string? Unit { get; set; }

    [JsonPropertyName("info")]
    public string? Info { get; set; }

    [JsonPropertyName("specLow")]
    public string? SpecLow { get; set; }

    [JsonPropertyName("specHi")]
    public string? SpecHi { get; set; }

    [JsonPropertyName("tactTime")]
    [MaxLength(50)]
    public string? TactTime { get; set; }

    [JsonPropertyName("usbSerial")]
    [MaxLength(50)]
    public string? UsbSerial { get; set; }

    [JsonPropertyName("orderNo")]
    [MaxLength(50)]
    public string? OrderNo { get; set; }

    [JsonPropertyName("destination")]
    [MaxLength(100)]
    public string? Destination { get; set; }

    [JsonPropertyName("romVersion")]
    [MaxLength(20)]
    public string? RomVersion { get; set; }

    [JsonPropertyName("ref4Value")]
    [MaxLength(100)]
    public string? Ref4Value { get; set; }

    [JsonPropertyName("ref5Value")]
    [MaxLength(100)]
    public string? Ref5Value { get; set; }

    [JsonPropertyName("noticeConditionFlg")]
    public string? NoticeConditionFlg { get; set; }

    [JsonPropertyName("countClearFlg")]
    [MaxLength(100)]
    public string? CountClearFlg { get; set; }

    [JsonPropertyName("dataInfo")]
    [MaxLength(50)]
    public string? DataInfo { get; set; }
}

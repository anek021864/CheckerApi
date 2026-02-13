using System.Text.Json.Serialization;

namespace JigNetApi.Dtos;

public class TIotMasterResponseDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("productSn")]
    public string? ProductSn { get; set; }

    [JsonPropertyName("computerName")]
    public string? ComputerName { get; set; }

    [JsonPropertyName("location")]
    public decimal? Location { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("productionLine")]
    public string? ProductionLine { get; set; }

    [JsonPropertyName("result")]
    public decimal? Result { get; set; }

    [JsonPropertyName("failCode")]
    public string? FailCode { get; set; }

    [JsonPropertyName("checkerName")]
    public string? CheckerName { get; set; }

    [JsonPropertyName("productionTime")]
    public DateTime? ProductionTime { get; set; }

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
    public string? FailName { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; set; }

    [JsonPropertyName("finishTime")]
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
    public string? TactTime { get; set; }

    [JsonPropertyName("usbSerial")]
    public string? UsbSerial { get; set; }

    [JsonPropertyName("orderNo")]
    public string? OrderNo { get; set; }

    [JsonPropertyName("destination")]
    public string? Destination { get; set; }

    [JsonPropertyName("romVersion")]
    public string? RomVersion { get; set; }

    [JsonPropertyName("ref4Value")]
    public string? Ref4Value { get; set; }

    [JsonPropertyName("ref5Value")]
    public string? Ref5Value { get; set; }

    [JsonPropertyName("noticeConditionFlg")]
    public string? NoticeConditionFlg { get; set; }

    [JsonPropertyName("countClearFlg")]
    public string? CountClearFlg { get; set; }

    [JsonPropertyName("dataInfo")]
    public string? DataInfo { get; set; }
}

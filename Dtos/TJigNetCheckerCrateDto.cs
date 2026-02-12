using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JigNetApi;

public class TJigNetCheckerCrateDto
{
    [JsonPropertyName("id")]
    [Required]
    [MaxLength(255)]
    public string? ID { get; set; }

    [JsonPropertyName("ProductSN")]
    [Required]
    [MaxLength(255)]
    public string? PRODSERIALNO { get; set; }

    [JsonPropertyName("computerName")]
    [Required]
    [MaxLength(255)]
    public string? COMPUTERNAME { get; set; }

    [JsonPropertyName("model")]
    [Required]
    [MaxLength(255)]
    public string? MODEL { get; set; }

    [JsonPropertyName("cell")]
    [Required]
    [MaxLength(255)]
    public string? CELL { get; set; }

    [JsonPropertyName("result")]
    [Required]
    [MaxLength(255)]
    public string? RESULTPASSFAIL { get; set; }

    [JsonPropertyName("failNumber")]
    [MaxLength(255)]
    public string? FAILNUMBER { get; set; }

    [JsonPropertyName("jigName")]
    [Required]
    [MaxLength(255)]
    public string? JIGNAME { get; set; }

    [JsonPropertyName("dateCheck")]
    [Required]
    [MaxLength(255)]
    public string? DATECHECK { get; set; }

    [JsonPropertyName("timeFinish")]
    [Required]
    [MaxLength(255)]
    public string? TIMEFINISH { get; set; }

    [JsonPropertyName("wirelessNo")]
    [Required]
    [MaxLength(255)]
    public string? WIRESSNO { get; set; }

    [JsonPropertyName("programVerion")]
    [Required]
    [MaxLength(255)]
    public string? PROGRAMVER { get; set; }

    [JsonPropertyName("steplistVersion")]
    [Required]
    [MaxLength(255)]
    public string? STEPLISTVER { get; set; }

    [JsonPropertyName("allDataLog")]
    [Required]
    public string? ALLDATALOG { get; set; }

    [JsonPropertyName("ngName")]
    [Required]
    public string? NGNAME { get; set; }

    [JsonPropertyName("position")]
    [Required]
    public string? POSITION { get; set; }

    [JsonPropertyName("cht")]
    [Required]
    public string? CHT { get; set; }

    [JsonPropertyName("testName")]
    [Required]
    public string? TESTNAME { get; set; }

    [JsonPropertyName("testNo")]
    [Required]
    public string? TESTNO { get; set; }

    [JsonPropertyName("specHi")]
    [Required]
    public string? SPECHI { get; set; }

    [JsonPropertyName("specLow")]
    [Required]
    public string? SPECLOW { get; set; }

    [JsonPropertyName("data")]
    [Required]
    public string? DATA { get; set; }
}

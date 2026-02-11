namespace JigNetApi;

public class TJigNetCheckerCrateDto
{
    [JsonPropertyName("productSn")]
    [Required]
    [MaxLength(100)]
    public string? ProductSn { get; set; }

    [JsonPropertyName("computerName")]
    [Required]
    [MaxLength(11)]
    public string? ComputerName { get; set; }
}

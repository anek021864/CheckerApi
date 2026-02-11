using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JigNetApi.Dtos;

public class DownLoadSteplistDto
{
    [JsonPropertyName("Model")]
    [Required]
    [MaxLength(20)]
    public string? Model { get; set; }

    [JsonPropertyName("CheckerName")]
    [Required]
    [MaxLength(500)]
    public string? CheckerName { get; set; }

    [JsonPropertyName("CheckerModel")]
    public decimal? CheckerModel { get; set; }

    [JsonPropertyName("Path")]
    [Required]
    public string? Path { get; set; }
}

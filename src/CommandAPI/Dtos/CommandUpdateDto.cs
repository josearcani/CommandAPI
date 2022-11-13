using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos;

public class CommandUpdateDto
{
    [Required]
    [MaxLength(250)]
    public string HowTo { get; set; } = string.Empty;

    [Required]
    public string Platform { get; set; } = string.Empty;

    [Required]
    public string CommandLine { get; set; } = string.Empty;
}
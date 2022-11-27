using System.ComponentModel.DataAnnotations;

namespace Infrastructure.FM;
public class DatabaseConfiguration
{
    [Required] public string ConnectionString { get; set; } = default!;
}
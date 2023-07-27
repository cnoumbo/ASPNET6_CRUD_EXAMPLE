using System.ComponentModel.DataAnnotations;

namespace Entities;

/// <summary>
/// Domain Model for Country
/// </summary>
public class Country
{
    [Key]
    public Guid country_id { get; set; }
    [StringLength(50)]
    public string? country_name { get; set; }
}


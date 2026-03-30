using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public Guid ClientRequestId { get; set; }

    public string Reference { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(10)]
    public string Currency { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
using System.ComponentModel.DataAnnotations;

namespace Shared;

public class TicketCreateModel
{
    public int Id { get; set; }

    [Required]
    [Range(1,50000)]
    public int Limit { get; set; }
    
    [Required]
    [StringLength(20,MinimumLength = 6)]

    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1,99999,ErrorMessage ="1 - 99999 arasında rakam girin.")]
    public double Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EventDate { get; set; }
    // public string Url { get; set; } = string.Empty;

    [Required]
    public int AddressId { get; set; }

    [Required]
    public int ActivityId { get; set; }

    [Required]
    public List<int>? ArtorsIds { get; set; }
}

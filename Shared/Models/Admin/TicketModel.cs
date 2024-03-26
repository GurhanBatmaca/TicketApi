using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared;

public class TicketModel
{
    public int Id  { get; set; }

    [Required]
    [Range(1,50000)]
    public int Limit { get; set; }
    
    [Required]
    [Range(1,50000)]
    public int FrontView { get; set; }

    [Required]
    [Range(1,50000)]
    public int MiddleView { get; set; }
    
    [Required]
    [Range(1,50000)]
    public int BackView { get; set; }
    
    [Required]
    [StringLength(20,MinimumLength = 6)]

    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1,99999,ErrorMessage ="1 - 99999 arasında rakam girin.")]
    public double Price { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EventDate { get; set; }

    [Required]
    public int AddressId { get; set; }

    [Required]
    public int ActivityId { get; set; }

    [Required]
    public List<int>? ArtorsIds { get; set; }
    public IFormFile? Image { get; set; }
}

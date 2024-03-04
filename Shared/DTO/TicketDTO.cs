namespace Shared;

public class TicketDTO
{
    public string Name { get; set; } = string.Empty;
    public string? EventDate { get; set; }
    public double Price { get; set; }
    
    public ActivityDTO? Activity { get; set; }
    public AddressDTO? Address { get; set; }
    public List<ArtorDTO> Artors { get; set; } = [];

}
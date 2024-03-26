namespace Shared;

public class TicketDTO
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string EventDate { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Limit { get; set; }
    
    public SeatInfoDTO? SeatInfo { get; set; }
    public ActivityDTO? Activity { get; set; }
    public AddressDTO? Address { get; set; }
    public List<ArtorDTO> Artors { get; set; } = [];

}
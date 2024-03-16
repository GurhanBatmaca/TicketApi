namespace Shared;

public class TicketModel
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public int Limit { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Url { get; set; } = string.Empty;

    public int AddressId { get; set; }
    public int ActivityId { get; set; }

    public List<int>? ArtorsIds { get; set; }
}

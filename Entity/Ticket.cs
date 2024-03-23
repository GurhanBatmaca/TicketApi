namespace Entity;

public class Ticket
{
    public int Id { get; set; }
    public int Limit { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Url { get; set; } = string.Empty;
    
    public int AddressId { get; set; }
    public Address? Address { get; set; }
    public int ActivityId { get; set; }
    public Activity? Activity { get; set; }
    public List<ReservedSeat> ReservedSeats { get; set; } = [];
    public List<TicketArtor> TicketArtors { get; set; } = [];
}

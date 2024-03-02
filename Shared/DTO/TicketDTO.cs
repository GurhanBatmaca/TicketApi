namespace Shared;

public class TicketDTO
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime EventDate { get; set; }

    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public string Activity { get; set; } = string.Empty;
    public List<string> Artors { get; set; } = [];

}
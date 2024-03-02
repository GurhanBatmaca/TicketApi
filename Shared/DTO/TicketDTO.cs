namespace Shared;

public class TicketDTO
{
    public string Activity { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? EventDate { get; set; }
    public double Price { get; set; }

    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public List<string> Artors { get; set; } = [];

}
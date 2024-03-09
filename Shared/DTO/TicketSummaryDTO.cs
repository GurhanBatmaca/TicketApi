namespace Shared;

public class TicketSummaryDTO
{
    public string Name { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Price { get; set; }

    
}

namespace Entity;

public class Address
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public int CityId { get; set; }
    public City? City { get; set; }
    public List<Ticket> Tickets { get; set; } = [];
}

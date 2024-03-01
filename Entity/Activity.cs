namespace Entity;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<Ticket> Tickets { get; set; } = [];
    public List<ActivityCategory> ActivityCategories { get; set; } = [];
}

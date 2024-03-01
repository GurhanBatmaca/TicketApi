namespace Entity;

public class Artor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<TicketArtor> TicketArtors { get; set; } = [];
    public List<ArtorWork> ArtorWorks { get; set; } = [];
}

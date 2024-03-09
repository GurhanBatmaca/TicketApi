namespace Entity;

public class Work
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<ArtorWork> ArtorWorks { get; set; } = [];

}

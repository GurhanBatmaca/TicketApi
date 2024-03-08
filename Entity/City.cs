namespace Entity;

public class City
{
    public int Id { get; set; }
    public int  PlateNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<Address>? Addresses { get; set; } = [];
}

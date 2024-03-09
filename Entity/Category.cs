namespace Entity;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<ActivityCategory> ActivityCategories { get; set; } = [];
}

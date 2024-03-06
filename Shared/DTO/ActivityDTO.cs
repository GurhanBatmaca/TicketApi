namespace Shared;

public class ActivityDTO
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<CategoryDTO>? Categories { get; set; } = [];
}

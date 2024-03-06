using Shared.Helpers;

namespace Shared;

public class SearchModel
{
    public string Query { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double? Price { get; set; }

}

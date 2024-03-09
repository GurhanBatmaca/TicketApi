using Entity;

namespace Shared;

public class AddressDTO
{
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public CityDTO? City { get; set; }
}

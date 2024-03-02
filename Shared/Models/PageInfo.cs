namespace Shared;
public class PageInfo
{
    public int TotalItems { get; set; }
    public int ItemPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int TotalPages()
    {
        return (int)Math.Ceiling((decimal)TotalItems/ItemPerPage);
    }
}

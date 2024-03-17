namespace Shared;

public class SuccessResponse
{
    public object Data { get; set; } = new object();
    public PageInfo? PageInfo { get; set; }
    public string? Message { get; set; }
}

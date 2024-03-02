namespace Entity;

public class ActivityCategory
{
    public int ActivityId { get; set; }
    public Activity? Activity { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}

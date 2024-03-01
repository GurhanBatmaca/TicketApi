namespace Entity;

public class ArtorWork
{
    public int ArtorId { get; set; }
    public Artor Artor { get; set; } = new Artor();

    public int WorkId { get; set; }
    public Work Work { get; set; } = new Work();
}

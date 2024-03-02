namespace Entity;

public class TicketArtor
{
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }

    public int ArtorId { get; set; }
    public Artor? Artor { get; set; }
}

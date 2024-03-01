namespace Entity;

public class TicketArtor
{
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = new Ticket();

    public int ArtorId { get; set; }
    public Artor Artor { get; set; } = new Artor();
}

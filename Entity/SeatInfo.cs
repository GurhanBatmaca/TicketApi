namespace Entity;

public class SeatInfo
{
    public int Id { get; set; }
    public int FrontLine { get; set; }
    public int MiddleLine { get; set; }
    public int BackLine { get; set; }

    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}

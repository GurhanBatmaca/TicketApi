namespace Entity;

public class SeatInfo
{
    public int Id { get; set; }
    public int FrontView { get; set; }
    public int MiddleView { get; set; }
    public int BackView { get; set; }

    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}

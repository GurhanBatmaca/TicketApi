namespace Entity;

public class ReservedSeat
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public DateTime SoldDate { get; set; }
    public SeatType SeatType { get; set; }

    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}

public enum SeatType 
{
    FrontLine = 0,
    MiddleLine = 1,
    BackLine = 2

}

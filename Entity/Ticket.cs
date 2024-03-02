﻿namespace Entity;

public class Ticket
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public int Limit { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime EventDate { get; set; }

    public int AddressId { get; set; }
    public Address? Address { get; set; }
    public int ActivityId { get; set; }
    public Activity? Activity { get; set; }

    public List<TicketArtor> TicketArtors { get; set; } = [];
}

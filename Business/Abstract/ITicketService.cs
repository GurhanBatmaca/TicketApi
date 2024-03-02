using Entity;
using Shared;

namespace Business;

public interface ITicketService: IService<Ticket>
{
    Task<List<TicketDTO>?> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
}

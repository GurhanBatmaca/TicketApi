using Entity;
using Shared;

namespace Data;

public interface ITicketRepository: IRepository<Ticket>
{
    Task<List<TicketDTO>> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
}

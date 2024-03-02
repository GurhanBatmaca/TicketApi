using Entity;

namespace Data;

public class EfCoreTicketRepository: EfCoreGenericRepository<Ticket>,ITicketRepository
{
    public EfCoreTicketRepository(StoreContext context):base(context)
    {
    }

    protected StoreContext? Context => _context as StoreContext;
}

namespace Data;

public class UnitOfWork : IUnitOfWork
{
    protected private StoreContext? _context;
    public UnitOfWork(StoreContext? context)
    {
        _context = context;
    }
    protected private EfCoreTicketRepository? ticketRepository;
    public ITicketRepository Tickets => ticketRepository ?? new EfCoreTicketRepository(_context!);

    public void Dispose()
    {
        _context!.Dispose();
    }
}

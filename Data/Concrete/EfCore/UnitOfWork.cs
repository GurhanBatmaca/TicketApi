using AutoMapper;

namespace Data;

public class UnitOfWork : IUnitOfWork
{
    protected private StoreContext? _context;
    protected readonly IMapper? _mapper;
    public UnitOfWork(StoreContext? context,IMapper? mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    protected private EfCoreTicketRepository? ticketRepository;
    public ITicketRepository Tickets => ticketRepository ?? new EfCoreTicketRepository(_context!,_mapper);

    public void Dispose()
    {
        _context!.Dispose();
    }
}

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
    protected private EfCoreActivityRepository? activityRepository;
    protected private EfCoreAddressRepository? addressRepository;
    public ITicketRepository Tickets => ticketRepository ?? new EfCoreTicketRepository(_context!,_mapper);

    public IActivityRepository Activities => activityRepository ?? new EfCoreActivityRepository(_context!,_mapper);

    public IAddressRepository Addresses => addressRepository ??  new EfCoreAddressRepository(_context!,_mapper);

    public void Dispose()
    {
        _context!.Dispose();
    }
}

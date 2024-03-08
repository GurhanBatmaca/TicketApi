namespace Data;

public interface IUnitOfWork: IDisposable
{
    ITicketRepository Tickets {get;}
    IActivityRepository Activities {get;}
    IAddressRepository Addresses {get;}
}

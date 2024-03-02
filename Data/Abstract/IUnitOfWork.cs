namespace Data;

public interface IUnitOfWork: IDisposable
{
    ITicketRepository Tickets {get;}
}

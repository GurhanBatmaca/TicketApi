namespace Data;

public interface IUnitOfWork: IDisposable
{
    ITicketRepository Tickets {get;}
    IActivityRepository Activities {get;}
    IAddressRepository Addresses {get;}
    IArtorRepository Artors {get;}
    ISignRepository Signs {get;}
    IUserRepository Users {get;}
}

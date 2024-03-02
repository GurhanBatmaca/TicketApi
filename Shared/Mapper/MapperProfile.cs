using AutoMapper;
using Entity;

namespace Shared;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Ticket,TicketDTO>();
    }
}

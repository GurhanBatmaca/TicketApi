using AutoMapper;
using Entity;

namespace Shared;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Activity,ActivityDTO>();
        CreateMap<Address,AddressDTO>();
        CreateMap<Artor,ArtorDTO>();
        CreateMap<Category,CategoryDTO>();
        CreateMap<City,CityDTO>();
        CreateMap<Ticket,TicketDTO>();

        CreateMap<Activity,ActivitySummaryDTO>();
    }
}

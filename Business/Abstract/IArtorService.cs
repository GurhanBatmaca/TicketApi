using Entity;
using Shared;

namespace Business;

public interface IArtorService: IService<Artor>
{
    Task<List<ArtorSummaryDTO>> GetAll();
    Task<List<ArtorDTO>> GetAllWithWorks();
}

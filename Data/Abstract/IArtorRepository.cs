using Entity;
using Shared;

namespace Data;

public interface IArtorRepository : IRepository<Artor>
{
    Task<List<ArtorSummaryDTO>> GetAll();
    Task<List<ArtorDTO>> GetAllWithWorks();
}

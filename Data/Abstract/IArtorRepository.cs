using Entity;
using Shared;

namespace Data;

public interface IArtorRepository : IRepository<Artor>
{
    Task<List<Artor>> GetAll();
    Task<List<Artor>> GetAllWithWorks();
    Task<Artor?> GetById(int id);
}

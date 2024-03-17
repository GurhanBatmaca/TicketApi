using Entity;
using Shared;

namespace Business;

public interface IArtorService: IService
{
    Task<bool> GetAll();
    Task<bool> GetAllWithWorks();
    Task<bool> GetById(int id);
}

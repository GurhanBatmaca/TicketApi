using Entity;
using Shared;

namespace Data;

public interface IActivityRepository: IRepository<Activity>
{
    Task<List<Activity>> GetAll();
    Task<List<Activity>> GetAllWithCategories();
    Task<Activity?> GetById(int id);
}

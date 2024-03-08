using Entity;
using Shared;

namespace Data;

public interface IActivityRepository: IRepository<Activity>
{
    Task<List<ActivitySummaryDTO>> GetAll();
    Task<List<ActivityDTO>> GetAllWithCategories();
    Task<ActivityDTO> GetById(int id);
}

using Entity;
using Shared;

namespace Business;

public interface IActivityService: IService<Activity>
{
    Task<List<ActivitySummaryDTO>> GetAll();
    Task<List<ActivityDTO>> GetAllWithCategories();
    Task<ActivityDTO> GetById(int id);
}

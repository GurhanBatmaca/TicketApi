using Entity;
using Shared;

namespace Business;

public interface IActivityService: IService
{
    Task<bool> GetAll();
    Task<bool> GetAllWithCategories();
    Task<bool> GetById(int id);
}

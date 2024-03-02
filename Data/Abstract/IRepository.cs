namespace Data;

public interface IRepository<T>
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T?> GetById(int id);
    Task<List<T>?> GetAll();
}

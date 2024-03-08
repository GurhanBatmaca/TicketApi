namespace Business;

public interface IService<T>
{
    string? Message { get; set; }
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
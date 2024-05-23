using GasStation.Domain.Entities;

namespace GasStation.Data.Interfaces;

public interface IGenericRepository<T> where T : Base
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

}

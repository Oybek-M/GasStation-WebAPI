using Microsoft.EntityFrameworkCore;
using GasStation.Data.DbContexts;
using GasStation.Data.Interfaces;
using GasStation.Domain.Entities;


namespace GasStation.Data.Repositories;

public class GenericRepository<T>(AppDbContext dbContext)
    : IGenericRepository<T> where T : Base
{
    protected readonly AppDbContext _dbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();



    public async Task<List<T>> GetAllAsync()
    {
        var res = await _dbSet.ToListAsync();
        return res;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var res = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }

    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}

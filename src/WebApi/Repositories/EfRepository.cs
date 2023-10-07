using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories;

public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DbContext _dataContext;

    public EfRepository(DbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dataContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetAsync(long id)
    {
        return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dataContext.Set<T>().AddAsync(entity);
        await _dataContext.SaveChangesAsync();

        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new System.NotImplementedException();
    }
}
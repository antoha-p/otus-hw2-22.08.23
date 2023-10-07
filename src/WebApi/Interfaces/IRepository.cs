using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetAsync(long id);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(long id);
}
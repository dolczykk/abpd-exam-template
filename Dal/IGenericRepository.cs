using Apbd.Dal.Entities;

namespace Apbd.Dal;

public interface IGenericRepository<T> where T : notnull, BaseEntity
{
    Task<IReadOnlyList<T>> GetAll();
    Task<T> GetById(Guid id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(Guid id);
    IQueryable<T> AsQueryable();
}
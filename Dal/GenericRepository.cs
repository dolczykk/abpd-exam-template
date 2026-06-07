using Apbd.Dal.Entities;

using Microsoft.EntityFrameworkCore;

namespace Apbd.Dal;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : notnull, BaseEntity
{
    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await context.Set<T>().FirstAsync(e => e.Id == id);
    }

    public async Task Add(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public async Task Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);

        context.Set<T>().Remove(entity);
    }

    public IQueryable<T> AsQueryable()
    {
        return context.Set<T>();
    }
}
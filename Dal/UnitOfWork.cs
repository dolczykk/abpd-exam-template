namespace Apbd.Dal;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}
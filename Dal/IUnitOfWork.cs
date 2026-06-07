namespace Apbd.Dal;

public interface IUnitOfWork
{
    Task SaveChanges();
}
namespace Fp.Api.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}

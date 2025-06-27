namespace Fp.Api.Persistence;

public interface IRepository<TModel> where TModel : class
{
    TModel? GetById(int id);
    IQueryable<TModel> Get();
    void Add(TModel entity);
    bool Remove(int id);
    void Update(TModel entity);
}

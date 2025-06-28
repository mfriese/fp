namespace Fp.Api.Persistence;

public class Repository<TModel>(TodoDbContext context) : IRepository<TModel>
    where TModel : class
{
    private TodoDbContext DbContext { get; } = context;

    public TModel? GetById(int id)
        => DbContext.Set<TModel>().Find(id);

    public IQueryable<TModel> Get()
        => DbContext.Set<TModel>();

    public void Add(TModel entity)
        => DbContext.Set<TModel>().Add(entity);

    public bool Remove(int id)
    {
        var set = DbContext.Set<TModel>();

        if (set.Find(id) is TModel model && model is not null)
        {
            set.Remove(model);

            return true;
        }

        return false;
    }
}

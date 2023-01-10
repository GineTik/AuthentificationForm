namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IRepository<TEntity, TEntity> { }

    public interface IRepository<TReturn, TParameter> 
    {
        TReturn? Get(long id);
        TReturn Add(TParameter entity);
        TReturn Remove(long id);
        TReturn Update(TParameter entity);
        IEnumerable<TReturn> GetAll();
    }
}

namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IRepository<TEntity, TEntity> { }

    public interface IRepository<TReturn, TParameter> 
    {
        TReturn? Get(int id);
        TReturn Add(TParameter entity);
        TReturn Remove(int id);
        TReturn Update(TParameter entity);
        IList<TReturn> GetAll();
    }
}

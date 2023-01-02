namespace AuthentificationForm.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        TEntity Remove(int id);
        TEntity Update(TEntity entity);
        IList<TEntity> GetAll();
    }
}

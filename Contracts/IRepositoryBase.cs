using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
      IQueryable<T> FindAll(bool trackChanges);
      IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression,bool trackChanges);
      void Create(T enity);
      void Update(T enity);
      void Delete(T enity);
    }
}

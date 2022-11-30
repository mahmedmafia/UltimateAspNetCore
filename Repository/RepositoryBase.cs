using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ?
            RepositoryContext.Set<T>() :
            RepositoryContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ?
             RepositoryContext.Set<T>().Where(expression) :
             RepositoryContext.Set<T>().Where(expression).AsNoTracking();

        }
        public void Create(T enity)
        {
            RepositoryContext.Set<T>().Add(enity);
        }

        public void Delete(T enity)
        {
            RepositoryContext.Set<T>().Remove(enity);

        }
        public void Update(T enity)
        {
            RepositoryContext.Set<T>().Update(enity);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Get(object id);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Insert(TEntity model, bool persist = false);

        Task<TEntity> Update(TEntity model, bool persist = false);

        Task<bool> Delete(long id, bool persist = false);

        Task<bool> Delete(TEntity model, bool persist = false);
    }
}

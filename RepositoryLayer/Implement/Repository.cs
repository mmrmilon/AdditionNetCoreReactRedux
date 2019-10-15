using DomainLayer;
using DomainLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryLayer.Implement
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DatabaseContext context;
        private readonly DbSet<TEntity> table;

        public Repository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> Get(object id)
        {
            return await table.FindAsync(id);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await table.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> Insert(TEntity model, bool persist = false)
        {
            await table.AddAsync(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<TEntity> Update(TEntity model, bool persist = false)
        {
            context.Set<TEntity>().Update(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(long id, bool persist = false)
        {
            var entity = Get(id);
            table.Attach(await entity);

            context.Entry(entity).State = EntityState.Deleted;
            return await context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> Delete(TEntity model, bool persist = false)
        {
            context.Entry(model).State = EntityState.Deleted;
            return await context.SaveChangesAsync() >= 1;
        }
    }
}

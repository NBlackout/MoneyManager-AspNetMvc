using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using NBlackout.MoneyManager.Entity;
using NBlackout.MoneyManager.Models;

namespace NBlackout.MoneyManager.Dal.Common
{
    public class BaseDao<T> : IDisposable where T : BaseModel
    {
        public MoneyManagerDbContext context;

        public BaseDao() : this(new MoneyManagerDbContext())
        {
        }

        public BaseDao(MoneyManagerDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Find()
        {
            return context.Set<T>();
        }

        public async Task<T> FindByIdAsync(long id)
        {
            var account = await context.Set<T>().FindAsync(id);

            return account;
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Set<T>().Add(entity);

            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            context.Entry(entity).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingEntity = await FindByIdAsync(entity.Id);
                if (existingEntity == null)
                    return false;

                throw;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
                return false;

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return true;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        #endregion
    }
}

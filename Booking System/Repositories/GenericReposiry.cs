
using Booking_System.Controllers;
using Booking_System.DataStore;
using Booking_System.Interfaces;
using Booking_System.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booking_System.Repositories
{
    public class GenericReposiry<T>:IGenericReposity<T> where T:BaseModel
    {
        public Context _Context { get; }

        public GenericReposiry(Context context)
        {
            _Context = context;
          
           
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _Context.Set<T>().AnyAsync(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _Context.Entry(entity).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _Context.Set<T>().Remove(entity);
                await _Context.SaveChangesAsync();
            }
        }

        Task IGenericReposity<T>.GetByIdAsync(int id)
        {
            return GetByIdAsync(id);
        }

        Task IGenericReposity<T>.ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return ExistsAsync(predicate);
        }

        Task IGenericReposity<T>.AddAsync(T entity)
        {
            return AddAsync(entity);
        }

        Task IGenericReposity<T>.UpdateAsync(T entity)
        {
            return UpdateAsync(entity);
        }
    }
}

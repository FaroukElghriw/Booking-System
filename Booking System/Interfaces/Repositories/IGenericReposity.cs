using Booking_System.Controllers;
using System.Linq.Expressions;

namespace Booking_System.Interfaces.Repositories
{
    public interface IGenericReposity<T> where T : BaseModel
    {
        Task GetByIdAsync(int id); 
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); 
        Task ExistsAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}

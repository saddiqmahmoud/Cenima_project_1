using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cenima_project.Repository.IRepositores
{
    public interface IRepositores<T> where T : class
    {
     Task CreateAsync(T entities);

        void Update(T enetites);

        void Delete(T enetites);

        Task Commit();

        Task<List<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool Tracked = true);

        Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool Tracked = true);
       
    }
}

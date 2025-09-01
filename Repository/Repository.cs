using Cenima_project.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cenima_project.Repository
{
    public class Repository<T>where T : class
    {
        private ApplicationdbContext _Context = new();
        private DbSet<T> _db;
        public Repository() {
            _db = _Context.Set<T>();
        }
        public async Task CreateAsync(T entities)
        { 
            await _db.AddAsync(entities);
        }
        public void Update(T enetites)
        {
            _db.Update(enetites);
        }
        public void Delete(T enetites)
        {
            _db.Remove(enetites);
        }
        public async Task Commit()
        {
            await _Context.SaveChangesAsync();
        }
        public async Task<List<T>> GetAsync(Expression<Func<T,bool>>? expression = null, Expression<Func<T, object>>[]? includes =null,bool Tracked=true)
        {
            var entites = _db.AsQueryable();
            if(expression is not null)
            {
                entites = _db.Where(expression);
            }
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    entites = _db.Include(item);
                }
            }
            if(!Tracked)
            {
                entites = _db.AsNoTracking();
            }
            return await entites.ToListAsync();
        }
        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null,bool Tracked=true)
        {
            return (await GetAsync(expression, includes,Tracked)).FirstOrDefault();
        }
    }
}

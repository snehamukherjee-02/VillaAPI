using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using practiceApi.Data;
using practiceApi.Models;
using practiceApi.Repository.IRepository;

namespace practiceApi.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Villa entity)
        {
            await _db.VillaTbl.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>>? filter = null, 
            Expression<Func<Villa, object>>? includes = null, Expression<Func<Villa, object>>? sort = null)
        {
            IQueryable<Villa> query = _db.VillaTbl;

            if (includes != null)
            {
                query = query.Include(includes);
            }

            if (sort != null)
            {
                query = query.OrderByDescending(sort);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>>? filter = null, 
            Expression<Func<Villa, object>>? includes = null, bool tracked = true)
        {

            IQueryable<Villa> query = _db.VillaTbl;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = query.Include(includes);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _db.VillaTbl.Remove(entity);
            await SaveAsync();
        }
        public async Task UpdateAsync(Villa entity)
        {
            _db.VillaTbl.Update(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

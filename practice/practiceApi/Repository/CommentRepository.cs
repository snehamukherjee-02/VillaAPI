using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using practiceApi.Data;
using practiceApi.Models;
using practiceApi.Repository.IRepository;

namespace practiceApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Comment entity)
        {
            await _db.CommentsTbl.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Comment>> GetAllAsync(Expression<Func<Comment, bool>>? filter = null)
        {
            IQueryable<Comment> query = _db.CommentsTbl;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Comment> GetAsync(Expression<Func<Comment, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<Comment> query = _db.CommentsTbl;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Comment entity)
        {
            _db.CommentsTbl.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment entity)
        {
            _db.CommentsTbl.Update(entity);
            await SaveAsync();
        } 
    }
}

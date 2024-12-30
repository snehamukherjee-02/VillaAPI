using System.Linq.Expressions;
using practiceApi.Models;

namespace practiceApi.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(Expression<Func<Comment,bool>>? filter = null);
        Task<Comment> GetAsync(Expression<Func<Comment, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(Comment entity);
        Task RemoveAsync(Comment entity);
        Task UpdateAsync(Comment entity);
        Task SaveAsync();
    }
}

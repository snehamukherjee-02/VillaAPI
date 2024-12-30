using System.Linq.Expressions;
using practiceApi.Models;

namespace practiceApi.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>>? filter = null,
            Expression<Func<Villa, object>>? includes = null, Expression<Func<Villa, object>>? sort = null);
        Task<Villa> GetAsync(Expression<Func<Villa, bool>>? filter = null, 
            Expression<Func<Villa, object>>? includes = null,bool tracked = true);
        Task CreateAsync(Villa entity);
        Task RemoveAsync(Villa entity);
        Task UpdateAsync(Villa entity);
        Task SaveAsync();

    }
}

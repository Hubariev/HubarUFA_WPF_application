using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagisterkaApp.Repo.Abstractions
{
    public interface IRepository<TEntity, Guid>
    {
        Task<List<TEntity>> GetAllAsync();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid type);
        Task<TEntity> GetById(Guid type);
    }
}

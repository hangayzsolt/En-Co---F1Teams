using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace F1Teams.DAL.Interfaces
{
    public interface IRepository<TDbContext,TEntity> 
        where TDbContext: DbContext 
        where TEntity: class 
    {
        Task<TEntity> GetById(int id, CancellationToken token);
        Task<List<TEntity>> GetAll(CancellationToken token);
        Task Create(TEntity entity, CancellationToken token);
        Task Update(TEntity entity, CancellationToken token);
        Task Delete(int id, CancellationToken token);
    }
}

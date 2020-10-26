using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using F1Teams.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace F1Teams.DAL.Implementations
{
    public class Repository<TDbContext,TEntity> : IRepository<TDbContext, TEntity>
        where TDbContext : DbContext
        where TEntity : class
    {
        private readonly TDbContext _context;

        public Repository(TDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAll(CancellationToken token)
        {
            return await _context.Set<TEntity>().ToListAsync(token);
        }

        public async Task<TEntity> GetById(int id, CancellationToken token)
        {
            return await this._context.Set<TEntity>().FindAsync(id);
        }

        public async Task Create(TEntity entity, CancellationToken token)
        {
            await _context.Set<TEntity>().AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task Update(TEntity entity, CancellationToken token)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var entity = await GetById(id, token);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(token);
        }

    }
}

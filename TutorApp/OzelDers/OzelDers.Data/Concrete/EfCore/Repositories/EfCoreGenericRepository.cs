using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OzelDers.Data.Abstract;

namespace OzelDers.Data.Concrete.EfCore.Repositories
{
    public class EfCoreGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class,
        new()
    {
         protected readonly DbContext _context;

        public EfCoreGenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            //_context.Entry(entity).State=EntityState.Modified;
        }

        public async Task UpdateAsyncc(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
            
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Infrastructure
{
    public interface IDataRepository
    {
        // temel crud işlemleri
        Task<IEnumerable<T>> GetAll<T>() where T : EntityBase;
        Task<T?> GetById<T>(long id) where T : EntityBase;
        Task<T> Add<T>(T entity) where T : EntityBase;
        Task<T> Update<T>(T entity) where T : EntityBase;
        Task<T> Delete<T>(T entity) where T : EntityBase;
    }
    internal class DataRepository : IDataRepository
    {
        private readonly DbContext _dbContext;

        public DataRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : EntityBase
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetById<T>(long id) where T : EntityBase
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> Add<T>(T entity) where T : EntityBase
        {
            entity.Id = default;
            entity.CreatedAt = DateTime.UtcNow;

            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update<T>(T entity) where T : EntityBase
        {
            if (entity.Id == default)
            {
                return null;
            }

            var dbEntity = await GetById<T>(entity.Id);
            if (dbEntity is null)
            {
                return null;
            }

            entity.CreatedAt = dbEntity.CreatedAt;

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async Task<T> Delete<T>(T entity) where T : EntityBase
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public interface IDataRepository
    {
        // temel crud işlemleri
        Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity;
        Task<T?> GetById<T>(int id) where T : BaseEntity;
        Task<T> Add<T>(T entity) where T : BaseEntity;
        Task<T> Update<T>(T entity) where T : BaseEntity;
        Task<T> Delete<T>(T entity) where T : BaseEntity;
    }
    internal class DataRepository : IDataRepository
    {
        private readonly DbContext _dbContext;

        public DataRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetById<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> Add<T>(T entity) where T : BaseEntity
        {
            entity.Id = default;
            entity.CreatedAt = DateTime.UtcNow;

            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update<T>(T entity) where T : BaseEntity
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
        public async Task<T> Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

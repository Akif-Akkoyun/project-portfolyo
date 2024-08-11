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
        IQueryable<T> GetAll<T>() where T : EntityBase;
        Task<T?> GetById<T>(long id) where T : EntityBase;
        Task<T> Add<T>(T entity) where T : EntityBase;
        Task<T> Update<T>(T entity) where T : EntityBase;
        Task<T> Delete<T>(T entity) where T : EntityBase;
    }
    public class DataRepository (DbContext context) : IDataRepository
    {
        public IQueryable<T> GetAll<T>() where T : EntityBase
        {
            return context.Set<T>();
        }
        public async Task<T> Add<T>(T entity) where T : EntityBase
        {
            entity.Id = default;
            entity.CreatedAt = DateTime.Now;

            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async Task<T> Delete<T>(T entity) where T : EntityBase
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<T?> GetById<T>(long id) where T : EntityBase
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T> Update<T>(T entity) where T : EntityBase
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

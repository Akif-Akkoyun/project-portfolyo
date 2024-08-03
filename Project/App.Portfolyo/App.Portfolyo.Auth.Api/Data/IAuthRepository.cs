using ImTools;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Auth.Api.Data.Entites;
using PortfolyoApp.Data;

namespace PortfolyoApp.Auth.Api.Data
{
    public interface IAuthRepository
    {
        IQueryable<T> GetAll<T>() where T : UserEntity;
        Task<T?> GetById<T>(long id) where T : UserEntity;
        Task<T> Add<T>(T entity) where T : UserEntity;
        Task<T> Update<T>(T entity) where T : UserEntity;
        Task<T> Delete<T>(T entity) where T : UserEntity;
    }
    internal class AuthRepository(DbContext context) : IAuthRepository
    {
        public IQueryable<T> GetAll<T>() where T : UserEntity
        {
            return context.Set<T>();
        }
        public async Task<T> Add<T>(T entity) where T : UserEntity
        {
            entity.Id = default;
            entity.CreatedAt = DateTime.UtcNow;
            entity.RoleId = 2;


            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async Task<T> Delete<T>(T entity) where T : UserEntity
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> GetById<T>(long id) where T : UserEntity
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T> Update<T>(T entity) where T : UserEntity
        {
            if (entity.Id == default)
            {
                throw new ArgumentException("Entity ID cannot be the default value.", nameof(entity));
            }

            var dbEntity = await GetById<T>(entity.Id);
            if (dbEntity is null)
            {
                throw new KeyNotFoundException($"Entity with ID {entity.Id} not found.");
            }

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}

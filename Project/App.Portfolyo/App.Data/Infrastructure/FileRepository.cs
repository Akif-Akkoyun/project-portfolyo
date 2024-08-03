using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Infrastructure
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileEntity>> GetAllFilesAsync();
        Task<FileEntity> GetFileByIdAsync(int id);
        Task AddFileAsync(FileEntity file);
        Task DeleteFileAsync(int id);
    }
    public class FileRepository : IFileRepository
    {
        private readonly DbContext _db;

        public FileRepository(DbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<FileEntity>> GetAllFilesAsync()
        {
            return await _db.Set<FileEntity>().ToListAsync();
        }

        public async Task<FileEntity> GetFileByIdAsync(int id)
        {
            return await _db.Set<FileEntity>().FindAsync(id) 
                ?? throw new InvalidOperationException("Id is null !!");
        }

        public async Task AddFileAsync(FileEntity file)
        {
            _db.Set<FileEntity>().Add(file);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(int id)
        {
            var file = await _db.Set<FileEntity>().FindAsync(id);
            if (file != null)
            {
                _db.Set<FileEntity>().Remove(file);
                await _db.SaveChangesAsync();
            }
        }
    }
}

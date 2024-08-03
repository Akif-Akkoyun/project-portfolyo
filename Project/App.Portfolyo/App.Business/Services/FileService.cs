using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PortfolyoApp.Business.Services
{
    public class FileService
    {
        private readonly FileRepository _fileRepository;

        public FileService(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<IEnumerable<FileEntity>> GetAllFilesAsync()
        {
            return await _fileRepository.GetAllFilesAsync();
        }

        public async Task<FileEntity> GetFileByIdAsync(int id)
        {
            return await _fileRepository.GetFileByIdAsync(id);
        }

        public async Task AddFileAsync(FileEntity file)
        {
            await _fileRepository.AddFileAsync(file);
        }

        public async Task DeleteFileAsync(int id)
        {
            await _fileRepository.DeleteFileAsync(id);
        }
    }
}

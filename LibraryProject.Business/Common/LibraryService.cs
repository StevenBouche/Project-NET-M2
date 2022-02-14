using LibraryProject.Domain.Common;
using LibraryProject.Infrastructure.Repositories.Common;
using Microsoft.Extensions.Logging;

namespace LibraryProject.Business.Common
{
    public abstract class LibraryService<T, K> : ILibraryService<T>
        where T : AuditableEntity
        where K : ILibraryRepository<T>
    {
        protected K Repository { get; }
        protected readonly ILogger Logger;

        public LibraryService(K repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public Task<T> CreateAsync(T value)
        {
            return Repository.AddAsync(value);
        }

        public Task<T> UpdateAsync(T value)
        {
            return Repository.UpdateAsync(value);
        }

        public Task<T> UpsertAsync(T value)
        {
            return Repository.UpsertAsync(value);
        }

        public Task<bool> DeleteAsync(T value)
        {
            return Repository.DeleteAsync(value);
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            return Repository.DeleteByIdAsync(id);
        }
    }
}
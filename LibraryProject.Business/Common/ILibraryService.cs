using LibraryProject.Domain.Common;

namespace LibraryProject.Business.Common
{
    public interface ILibraryService<T> where T : AuditableEntity
    {
        public Task<T> CreateAsync(T value);

        public Task<T> UpdateAsync(T value);

        public Task<T> UpsertAsync(T value);

        public Task<bool> DeleteAsync(T value);

        public Task<bool> DeleteByIdAsync(int id);
    }
}
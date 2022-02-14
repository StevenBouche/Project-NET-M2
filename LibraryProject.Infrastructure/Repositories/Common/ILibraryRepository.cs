using LibraryProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryProject.Infrastructure.Repositories.Common
{
    public interface ILibraryRepository<T> where T : AuditableEntity
    {
        DbSet<T> Set { get; }

        bool AnyById(int id);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetFirstByCriteria(Expression<Func<T, bool>> predicate);

        Task<bool> AnyByCriteria(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetPagination(int pageNumber, int pageSize);

        IEnumerable<T> GetPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);

        Task<bool> DeleteByIdAsync(int id);

        Task<bool> DeleteAsync(T entity);

        Task<T> UpsertAsync(T entity);

        Task<T> UpdateAsync(T entity);

        IEnumerable<T> FilterByCriteria(Func<T, bool> predicate);
    }
}
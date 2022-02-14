using LibraryProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Infrastructure.Repositories.Common
{
    public class LibraryRepository<T> : ILibraryRepository<T> where T : AuditableEntity
    {
        public DbSet<T> Set => throw new NotImplementedException();

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyByCriteria(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool AnyById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FilterByCriteria(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetFirstByCriteria(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPagination(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpsertAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
using LibraryProject.Domain.Common;

namespace LibraryProject.Business.Common
{
    public interface ILibraryService<T> where T : AuditableEntity
    {
    }
}
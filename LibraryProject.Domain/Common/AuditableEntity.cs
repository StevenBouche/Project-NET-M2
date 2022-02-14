using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Domain.Common
{
    public class AuditableEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
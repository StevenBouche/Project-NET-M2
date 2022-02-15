using LibraryProject.Domain.Common;
using LibraryProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Infrastructure.Persistence
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Book> Books => Set<Book>();
        public virtual DbSet<Genre> Genres => Set<Genre>();
        public virtual DbSet<BookGenre> BookGenres => Set<BookGenre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>(e =>
            {
                e.HasKey(bc => new { bc.BookId, bc.GenreId });

                e.HasOne(bc => bc.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bc => bc.BookId);

                e.HasOne(bc => bc.Genre)
                .WithMany(c => c.BookGenres)
                .HasForeignKey(bc => bc.GenreId);
            });

            modelBuilder.Entity<Genre>(g =>
            {
                g.HasIndex(genre => genre.Name).IsUnique();
            });

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditOnEntries();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetAuditOnEntries();
            return base.SaveChanges();
        }

        internal void SetAuditOnEntries()
        {
            // AutoDetectChanges is disabled so you need to update the tracker
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries().Where(t => t.Entity is AuditableEntity && (t.State == EntityState.Added || t.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var element = (AuditableEntity)entry.Entity;

                element.UpdatedAt = DateTimeOffset.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    element.CreatedAt = DateTimeOffset.UtcNow;
                };
            }
        }
    }
}
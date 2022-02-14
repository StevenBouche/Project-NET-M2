using LibraryProject.Domain.Entities;

namespace LibraryProject.Infrastructure.Persistence
{
    public class LibraryContextSeed
    {
        private readonly LibraryContext Context;

        public LibraryContextSeed(LibraryContext context)
        {
            Context = context;
        }

        public void SeedData()
        {
            AddUniqueBook(new Book()
            {
                Name = "Toto book",
                Author = "Toto",
                Price = 1000000000000,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            });

            Context.SaveChanges();
        }

        private void AddUniqueBook(Book book)
        {
            var currentUser = Context.Books.FirstOrDefault(u => u.Name == book.Name);
            if (currentUser == null)
            {
                Context.Books.Add(book);
            }
        }
    }
}
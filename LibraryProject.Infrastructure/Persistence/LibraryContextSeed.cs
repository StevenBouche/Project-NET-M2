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
            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    Name = "Livre1",
                    Author = "Moi",
                    Price = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Book()
                {
                    Name = "Livre2",
                    Author = "Moi",
                    Price = 2,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            };

            List<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    Name="SF",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Genre()
                {
                    Name="SFF",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            };

            foreach (var book in books)
            {
                AddUniqueBook(book);
            }

            foreach (var genre in genres)
            {
                AddUniqueGenre(genre);
            }

            Context.SaveChanges();

            AddUniqueBookGenre(genres[0], books[0]);

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

        private void AddUniqueGenre(Genre genre)
        {
            var currentUser = Context.Genres.FirstOrDefault(u => u.Name == genre.Name);
            if (currentUser == null)
            {
                Context.Genres.Add(genre);
            }
        }

        private void AddUniqueBookGenre(Genre genre, Book book)
        {
            if (genre.Id == 0 || book.Id == 0)
                return;

            var current = Context.BookGenres.FirstOrDefault(u => u.BookId == book.Id && u.GenreId == genre.Id);
            if (current == null)
            {
                Context.BookGenres.Add(new BookGenre() { BookId = book.Id, GenreId = genre.Id});
            }
        }
    }
}
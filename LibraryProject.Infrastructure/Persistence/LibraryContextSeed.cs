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

        public double GetRandomNumber(Random random, double minimum, double maximum)
        {
            
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public void SeedData()
        {

            int nbBook = 100;
            double priceMin = 0.01;
            double priceMax = 1000;
            Random random = new Random();
            List<Book> b = new List<Book>();

            List<Book> books = new List<Book>()
            {
                new Book()
                {
                    Name = "Livre_1",
                    Author = "Tata",
                    Price = 10,
                    Content = SeedConst.POEME_RICARD,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Book()
                {
                    Name = "Livre_2",
                    Author = "Toto",
                    Price = 20,
                    Content = SeedConst.POEME_FLEUR,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Book()
                {
                    Name = "Livre_3",
                    Author = "Titi",
                    Price = 30,
                    Content = SeedConst.POEME_VIE,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Book()
                {
                    Name = "Livre_4",
                    Author = "Tutu",
                    Price = 40,
                    Content = SeedConst.POEME_SOLEIL,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Book()
                {
                    Name = "Livre_5",
                    Author = "Tete",
                    Price = 50,
                    Content = SeedConst.POEME_BANDOL,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
            };

            for (int i = 0; i < nbBook; i++)
            {
                b.Add(new Book()
                {
                    Name = $"Livre_{6+i}",
                    Author = Faker.Name.FullName(),
                    Price = (double)((int)(GetRandomNumber(random, priceMin, priceMax)*100))/100,
                    Content = string.Join("\n\n", Faker.Lorem.Paragraphs(random.Next(1, 5))),
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                });
            }

            books.AddRange(b);

            List<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    Name="Polar",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Genre()
                {
                    Name="Roman",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Genre()
                {
                    Name="SF",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Genre()
                {
                    Name="Thriller",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Genre()
                {
                    Name="Essai",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Genre()
                {
                    Name="Bandol",
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

            foreach (var book in books)
            {
                var nbGenre = random.Next(1, 4);
                for(int i = 0; i < nbGenre; i++)
                {
                    var list = genres.Where(gr => !book.BookGenres.Any(bg => bg.GenreId == gr.Id)).ToList();
                    var g = list.ElementAt(random.Next(1, list.Count - 1));
                    AddUniqueBookGenre(g, book);
                }
            }

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
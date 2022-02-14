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
            //Context.SaveChanges();
        }
    }
}
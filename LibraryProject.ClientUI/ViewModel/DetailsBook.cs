using LibraryProject.Business.Dto.Books;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => { /* A vous de définir la commande */ });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public BookDetailsDto CurrentBook { get; set; }
        public string genres { get; set; }
        public int bookId { get; set; }

        public DetailsBook(int bookId)
        {
            this.bookId = bookId;

            Task.Run(async () =>
            {
                CurrentBook = await LibraryProject.API.Client.API.findById(this.bookId);
            for (int i = 0; i < CurrentBook.Genres.Count; i++)
                {
                    genres += CurrentBook.Genres[i].Name;
                    if (i != CurrentBook.Genres.Count)
                    {
                        genres += "/";
                    }
                }
                
            });
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(0) { }
    }
}

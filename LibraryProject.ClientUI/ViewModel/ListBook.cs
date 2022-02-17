using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Genres;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand BookSelectedCommand { get; set; }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public List<BookDto> baseBooksList = new List<BookDto>();
        public ObservableCollection<BookDto> Books { get; set; } = new ObservableCollection<BookDto>();
        public ObservableCollection<string> genreList { get; set; } = new ObservableCollection<string>();

        public ListBook()
        {
            Task.Run(async () =>
            {
                PaginationResultDto result = await LibraryProject.API.Client.API.search(1, 10);
                List<BookDto> resultBooks = result.Books;
                baseBooksList = resultBooks;

                Application.Current.Dispatcher.Invoke(() => {
                    foreach (var book in resultBooks) {
                        Books.Add(book);
                    }
                });


                List<GenreDto> genres = await LibraryProject.API.Client.API.getAllGenres();
                Application.Current.Dispatcher.Invoke(() => {
                    genreList.Add("All");
                    foreach (var g in genres)
                    {
                        genreList.Add(g.Name);
                    }
                });
            });

            ItemSelectedCommand = new RelayCommand(value => {
                if ((string)value == "All")
                {
                    Books.Clear();
                    foreach (var book in baseBooksList)
                    {
                        Books.Add(book);
                    }
                }
                else
                {
                    Books.Clear();
                    foreach (var book in baseBooksList)
                    {
                        foreach (var genre in book.Genres)
                        {
                            if ((string)value == genre.Name) Books.Add(book);
                        }
                    }
                }
            });

            BookSelectedCommand = new RelayCommand(book =>
            {
                if (book != null)
                {
                    Trace.WriteLine("selected book: " + ((BookDto)book).Name);
                }
            });
        }
    }
}

using LibraryProject.Business.Dto.Books;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<BookDto> Books { get; set; } = new ObservableCollection<BookDto>();

        public string TestString { get; set; }

        public ListBook()
        {
            TestString = "ok";
            Task.Run(async () =>
            {
                PaginationResultDto result = await LibraryProject.API.Client.API.search(1, 10);
                List<BookDto> resultBooks = result.Books;
                Trace.WriteLine("get by id: " + resultBooks.Count);

                Application.Current.Dispatcher.Invoke(() => {
                    foreach(var book in resultBooks) {
                        Books.Add(book);
                    }
                });
                TestString = "ko";
            });

            ItemSelectedCommand = new RelayCommand(book => { /* the livre devrais etre dans la variable book */ });
        }
    }
}

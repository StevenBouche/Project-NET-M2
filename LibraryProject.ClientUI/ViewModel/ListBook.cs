using LibraryProject.Business.Dto.Books;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
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
        public ObservableCollection<BookDetailsDto> Books { get; set; } = new ObservableCollection<BookDetailsDto>();

        public string TestString { get; set; }

        public ListBook()
        {
            TestString = "ok";
            Task.Run(async () =>
            {
                BookDetailsDto book = await LibraryProject.API.Client.API.findById(1);
                Trace.WriteLine("get by id: " + book.Name + book.Author);
                //Books.Add(book);
                Application.Current.Dispatcher.Invoke(() => { Books.Add(book); });
                TestString = "ko";
            });

            ItemSelectedCommand = new RelayCommand(book => { /* the livre devrais etre dans la variable book */ });
        }
    }
}

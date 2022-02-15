using LibraryProject.Business.Dto.Books;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public delegate void MyDelegate(string msg);
        MyDelegate del = new MyDelegate((string msg) =>
        {
            Trace.WriteLine("msg: " + msg);
        });

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                BookDetailsDto book = await LibraryProject.API.Client.API.findById(1);
                del.Invoke(book.Name);
            });
            
        }
    }
}

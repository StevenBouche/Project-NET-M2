using LibraryProject.Business.Dto.Books;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System;
using System.Collections.Generic;

namespace WPF.Reader.ViewModel
{
    public class ReadBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => { /* A vous de définir la commande */ });

        // n'oublier pas faire de faire le binding dans ReadBook.xaml !!!!
        public BookDetailsDto CurrentBook { get; set; }

        public string[] PagesContent { get; init; }

        public int PageCount { get; set; }

        public string PageContentLeft
        {
            get { return PagesContent[PageCount]; }
        }
        public string PageContentRight
        {
            get { 
                if(PageCount + 1 < PagesContent.Length)
                {
                   return PagesContent[PageCount + 1];
                }
                return ""; 
            }
        }
        public ICommand HandleNextPage { get; init; }
        public ICommand HandlePreviousPage { get; init; }

        public void NextPage()
        {
            if(PageCount + 1  < PagesContent.Length)
            {
                PageCount+=2;
            }
        }

        public void PreviousPage()
        {
            if(PageCount >= 2)
            {
               PageCount-=2;
            }
        }


        static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
        }

        public ReadBook(BookDetailsDto book)
        {
            HandleNextPage = new RelayCommand(x => {
                NextPage();
            }, x => PageCount + 2 < PagesContent.Length);

            HandlePreviousPage = new RelayCommand(x => {
                PreviousPage();
            }, x => PageCount >= 2);

            CurrentBook = book;
            IEnumerable<string> s = ChunksUpto(CurrentBook.Content, 580);
            PagesContent = s.ToArray();
            PageCount = 0;
        }

    }

}

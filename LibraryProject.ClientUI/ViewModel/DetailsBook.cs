﻿using LibraryProject.Business.Dto.Books;
using System.ComponentModel;
using System.Windows.Input;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => { /* A vous de définir la commande */ });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public BookDetailsDto CurrentBook { get; init; }

        public DetailsBook(BookDetailsDto book)
        {
            CurrentBook = book;
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new BookDetailsDto() /*{ Title = "Test Book" }*/) { }
    }
}

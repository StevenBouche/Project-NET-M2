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
        public BookDetailsDto CurrentBook { get; init; }

        public string[] PagesContent { get; init; }

        public int PageCount { get; set; }

        public void NextPage()
        {
            if(PageCount + 1  < PagesContent.Length)
            {
                PageCount++;
            }
        }

        public void PreviousPage()
        {
            if(PageCount > 0)
            {
               PageCount--;
            }
        }


        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public ReadBook()
        {
            CurrentBook = new BookDetailsDto() { Name="Les comptes de la fontaine", Content= @"LA CIGALE ET LA FOURMI
La Cigale,
                ayant chanté
Tout L'Été,
Se trouva fort dépourvue
Quand la Bise fut venue.
Pas un seul petit morceau
De mouche ou de vermisseau.
Elle alla crier famine
Chez la Fourmi sa voisine,
                La priant de lui prêter
Quelque grain pour subsister
Jusqu'à la saison nouvelle.
« Je vous paierai,
                lui dit - elle,
                Avant l'Août, foi d'animal,
                Intérêt et principal. »
La Fourmi n'est pas prêteuse :
C'est là son moindre défaut.
« Que faisiez - vous au temps chaud ?
Dit - elle à cette emprunteuse.
- Nuit et jour à tout venant
Je chantais,
                ne vous déplaise.
- Vous chantiez ? j'en suis fort aise :
Eh bien! dansez maintenant. »"};
            IEnumerable<string> s = Split(CurrentBook.Content, 10);
            PagesContent = s.ToArray();
            PageCount = 0;
        }

    }

}

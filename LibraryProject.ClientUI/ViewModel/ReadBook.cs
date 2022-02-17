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

        public ReadBook()
        {
            HandleNextPage = new RelayCommand(x => {
                NextPage();
            }, x => PageCount + 2 < PagesContent.Length);

            HandlePreviousPage = new RelayCommand(x => {
                PreviousPage();
            }, x => PageCount >= 2);

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
Eh bien! dansez maintenant. »



FABLE II
LE CORBEAU ET LE RENARD
Maître Corbeau, sur un arbre perché,
Tenait en son bec un fromage.
Maître Renard, par l'odeur alléché,
Lui tint à peu près ce langage :
« Et bonjour, Monsieur du Corbeau.
Que vous êtes joli ! que vous me semblez beau !
Sans mentir, si votre ramage
Se rapporte à votre plumage,
Vous êtes le Phénix des hôtes de ces Bois. »
A ces mots le corbeau ne se sent pas de joie :
Et pour montrer sa belle voix,
Il ouvre un large bec, laisse tomber sa proie.
Le Renard s'en saisit, et dit : « Mon bon Monsieur,
Apprenez que tout flatteur
Vit aux dépens de celui qui j'écoute.
cette leçon vaut bien un fromage sans doute. »
Le corbeau honteux et confus
Jura, mais un peu tard, qu'on ne l'y prendrait plus.
" };
            IEnumerable<string> s = ChunksUpto(CurrentBook.Content, 580);
            PagesContent = s.ToArray();
            PageCount = 0;
        }

    }

}

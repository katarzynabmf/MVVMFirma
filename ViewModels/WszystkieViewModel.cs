using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    // w klasie WszystkieViewModel będą znajdowały się te elementy które występuja w każdej klasie obsługującej 
    //dowolną kolekcję obiektów biznesowych
    //A zatem z klasy WszytskieViewModel będą dziedziczyły np. WszytskieTowaryViewModel, WszytskieZamowieniaViewModel,
    //WszyscyPracownicyViewModel
    public abstract class WszystkieViewModel<T>: WorkspaceViewModel
    {
        #region Fields
        // to jest pole odpowiedzialne za połączenie z baza danych
        protected readonly FakturyEntities fakturyEntities;
        //to jest commenda która wywoła ładowanie wszytskich towarów z bazy danych
        private BaseCommand _LoadCommand;
        //to jest komenda do wywoływania zakładki do wywoływania 
        private BaseCommand _AddCommand;
        // w tej kolekcji będą przechowywane wszytskie towary z bazy danych
        private ObservableCollection<T> _List;

        //to jest komenda która zostanie podpięta pod przycisk sortuj 
        private BaseCommand _SortCommand;
        //to jest komenda która zostanie podpięta pod przycisk szukaj w generic.xaml
        private BaseCommand _FindCommand;
        #endregion Fields
        #region Properties
        // ta komenda wywołuję metodę load
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => load());
                }
                return _LoadCommand;
            }
        }
        //to jest komenda która będzie podpięta pod przycisk plus i ta komenda wywoła metodę add z sekcji Helpers 
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => add());
                }
                return _AddCommand;
            }
        }

        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) load(); // jezeli lista jest pusta, to wywolujemy metode load
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        //w tej właściwości zostanie zapisane po czym sortować 
        public string SortField { get; set; }
        //w tej właściwości zostanie podana lista elementów wzgledem ktorych zostanie podana lista po czym sortować
        public List<string> SortComboboxItems
        {
            get
            {
                return GetComboboxSortList();
            }
             }

        // to jest komenda która zostanie podpięta pod przycisk sortuj
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                    _SortCommand = new BaseCommand(() => Sort());
                return _SortCommand;
            }
        }
        //w tej właściwości będziemy przechowywać wynik wyboru po czym wyszukiwac z comboboxa
        public string FindField { get; set; }
        //w tej właściwości będziemy pobierać wszytskie możliwości po których można wyszukiwać 
        //po to zeby cobobox wiedział po czym można wyszukiwać
        public List<string> FindComboboxItems
        {
            get
            {
                return GetComboboxFindList();
            }
        }
        //w tej właściwości będziemy przechowywać początek szukanej frazy
        public string FindTextBox { get; set; }
        //ta komenda będzie podpięta pod przycisk szukaj
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                    _FindCommand = new BaseCommand(() => Find());
                return _FindCommand;
            }
        }

        #endregion Properties

        #region Constructor
        public WszystkieViewModel()
        {
            fakturyEntities = new FakturyEntities();
        }
        #endregion Constructor
        #region Helper
       
        //ta metoda będzie okrślała jak sortować w klassach dziedizczących
        public abstract void Sort();
        //ta metoda będzie mówiła po czym sortowac w klasach dziedziczących
        public abstract List<string> GetComboboxSortList();
        // ta metoda będzie mówiła  jak wyszukiwać
        public abstract void Find();
        //ta metoda określa po czym wyszukiwać i będzie zdefiniowana w klasach dziedziczących
        public abstract List<string> GetComboboxFindList();
        //metoda load pobierze z bazy wszystkie towary i przypisze je do listy 
        public abstract void load();
        private void add()
        {
            //za pomocą messengera z biblioteki MvvmLight wyślemy do MainWindowViewModel stringa z poleceniem otwarcie okna
            Messenger.Default.Send(DisplayName + "Add"); //nie działa przez to ze nie mam wtyczki

        }


        #endregion Helpers


    }
}

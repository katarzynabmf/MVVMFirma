using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    //to jest klasa z której będą dziedziczyć view modele np.:
    //Faktur bo jest jedna faktura i jej wszytskie pozycjeZamowienieForAllView
    //Zamówień bo jest jedno zamówienie i wszytskie pozycje
    //Ofert bo jest jedna oferta i wszytskie pozycje
    //Paragonów bo jest jeden paragon i kilka pozycji
    //Delegacji bo jest jedna delegacja i jej wszytskie etapy
    public abstract class JedenWszystkieViewModel <J,W>: JedenViewModel<J>
    {
        #region Fields

        //to jest lista pozycji
        private ObservableCollection<W> _List;
        protected string DisplayListName; //nazwa listy pozycji
        private BaseCommand _ShowAddViewCommand; //to jest komenda któa wywoła okno do dodawania pozycji
        #endregion Fields
        #region Properties
       
        //komenda do wywolywania okna do dodawania pozycji
        public ICommand ShowAddViewCommand
        {
            get
            {
                if (_ShowAddViewCommand == null)
                {
                    _ShowAddViewCommand = new BaseCommand(() => ShowAddView());
                }
                return _ShowAddViewCommand;
            }
        }

        public ObservableCollection<W> List
        {
            get
            {
                if (_List == null) Load(); // jezeli lista jest pusta, to wywolujemy metode load
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion Properties

        #region Constructor
        public JedenWszystkieViewModel()
            :base ()
        {
        }
        #endregion Constructor
        #region Helper
        //metoda load pobierze z bazy wszystkie towary i przypisze je do listy 
        public abstract void Load();
        private void ShowAddView()
        {
            //ten messenger wysyła do MainWindowVieModel polecenie otworzenia okna do dodawania pozycji
            Messenger.Default.Send(DisplayListName + "Add"); 

        }

        #endregion
    }
}

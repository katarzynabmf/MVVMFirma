using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MVVMFirma.ViewModels
{
    public class WszyscyKontrahenciViewModel : WszystkieViewModel<KontrahenciForAllView>
    {

        #region Properties
        private KontrahenciForAllView _WybranyKontrahent;
        public KontrahenciForAllView WybranyKontrahent 
        {
            get
            {
                return _WybranyKontrahent;
            }
            set
            {
                if(_WybranyKontrahent !=value)
                {
                    _WybranyKontrahent = value;
                    //jak klikniemy w datagridzie na wybranego kontrhaneta to wywola się set WybranegoKontrahneta
                    //i należy tego kontrahenta messengerem wysłać do okna z nową fakturą 
                    Messenger.Default.Send(_WybranyKontrahent);
                    //po wybraniu kontrahenta zamykam oknno
                    OnRequestClose();
                }
            }
        }
        #endregion

        #region Konstruktor
        public WszyscyKontrahenciViewModel()
        {
            base.DisplayName = "Kontrahenci";
        }
        #endregion Konstruktor
        #region Helpers
        //metoda load pobierze z bazy wszystkie towary i przypisze je do listy 
        public override void load()
        {
            //za pomocą obiektu reprezentującego całą bazę danych o nazwie fakturyEntities pobieram wszystkie 
            //rekordy z tabeli Towary i zamieniam je na ObservableCollection
            List = new ObservableCollection<KontrahenciForAllView>
                (
                    //zapytanie pobiera z bazy danych wszystkie faktury i zapisuje je jako FakturyForAllView
                    from kontrahent in fakturyEntities.Kontrahenci //dla kazdej faktury z bazy danych faktur tworzymy nowa
                                                            //fakture for all view
                    select new KontrahenciForAllView
                    {//i wypelniamy jej dane
                        IdKontrahenta=kontrahent.IdKontrahenta,
                        Kod = kontrahent.Kod,
                        Nip= kontrahent.Nip,
                        Nazwa = kontrahent.Nazwa,
                        RodzajNazwa = kontrahent.Rodzaje.Nazwa,
                        StatusNazwa = kontrahent.Statusy.Nazwa,
                        Adres =
                        "ul. "
                        + kontrahent.Adresy.Ulica + " "
                        + kontrahent.Adresy.NrDomu + "/"
                        + kontrahent.Adresy.NrLokalu + " ",
                    }
                );
        }
        #endregion Helpers
        #region Sort and Find
        public override List<string> GetComboboxFindList()
        {
            return null;
        }

        public override void Find()
        {

        }

        public override List<string> GetComboboxSortList()
        {
            return null;
        }

        public override void Sort()
        {

        }
        #endregion
    }
}

using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MVVMFirma.ViewModels
{
    public class WszystkieFakturyViewModel: WszystkieViewModel<FakturaForAllView>
    {
        #region Konstruktor
        public WszystkieFakturyViewModel()
        {
            base.DisplayName = "Faktury";
        }
        #endregion Konstruktor
        #region Helpers
        //metoda load pobierze z bazy wszystkie towary i przypisze je do listy 
        public override void load()
        {
            //za pomocą obiektu reprezentującego całą bazę danych o nazwie fakturyEntities pobieram wszystkie 
            //rekordy z tabeli Towary i zamieniam je na ObservableCollection
            List = new ObservableCollection<FakturaForAllView>
                (
                    //zapytanie pobiera z bazy danych wszystkie faktury i zapisuje je jako FakturyForAllView
                    from faktura in fakturyEntities.Faktury //dla kazdej faktury z bazy danych faktur tworzymy nowa
                    //fakture for all view
                    select new FakturaForAllView
                    {//i wypelniamy jej dane
                        IdFaktury=faktura.IdFaktury,
                        Numer=faktura.Numer,
                        DataWystawienia=faktura.DataWystawienia,
                        KontrahentNazwa=faktura.Kontrahenci.Nazwa,
                        KontrahentNip=faktura.Kontrahenci.Nip,
                        KontrahentAdres=
                        "ul. "
                        +faktura.Kontrahenci.Adresy.Ulica+" "
                        + faktura.Kontrahenci.Adresy.NrDomu+"/"
                        + faktura.Kontrahenci.Adresy.NrLokalu+" "
                        + faktura.Kontrahenci.Adresy.KodPocztowy+" "
                        + faktura.Kontrahenci.Adresy.Poczta,
                        TerminPlatnosci=faktura.TerminPlatnosci,
                        SposobPlatnosciNazwa=faktura.IdSposobyPlatnosci.Nazwa,

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Models.Entities;
using MVVMFirma.Helper;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class WszystkieTowaryViewModel :WszystkieViewModel<Towary>
    {
       
        #region Constructor
        public WszystkieTowaryViewModel()
            :base()
        {
            //
            base.DisplayName = "Towary";
        }
        #endregion Constructor
        #region Helpers
        //metoda load pobierze z bazy wszystkie towary i przypisze je do listy 
        public override void load()
        {
            //za pomocą obiektu reprezentującego całą bazę danych o nazwie fakturyEntities pobieram wszystkie 
            //rekordy z tabeli Towary i zamieniam je na ObservableCollection
            List = new ObservableCollection<Towary>(fakturyEntities.Towary);
        }
        #endregion Helpers
        #region Sort and Find
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa", "Kod" };
        }

        public override void Find()
        {
            if (FindField == "Nazwa")
                List = new ObservableCollection<Towary>(List.Where(item=>item.Nazwa!=null && item.Nazwa.StartsWith(FindTextBox)));
            if (FindField == "Kod")
                List = new ObservableCollection<Towary>(List.Where(item => item.Kod != null && item.Kod.StartsWith(FindTextBox)));
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa", "Kod", "Cena", "Cena malejąco", "Nazwa i Cena" };
            
        }

        public override void Sort()
        {
            if (SortField == "Nazwa")
                List = new ObservableCollection<Towary>(List.OrderBy(item => item.Nazwa));
            if (SortField == "Kod")
                List = new ObservableCollection<Towary>(List.OrderBy(item => item.Kod));
            if (SortField == "Cena")
                List = new ObservableCollection<Towary>(List.OrderBy(item => item.Cena));
            if (SortField == "Nazwa i Cena")
                List = new ObservableCollection<Towary>(List.OrderBy(item => item.Nazwa).ThenBy(item => item.Cena));
            if (SortField == "Cena malejąco")
                List = new ObservableCollection<Towary>(List.OrderByDescending(item => item.Cena));
        }
        #endregion
    }
}
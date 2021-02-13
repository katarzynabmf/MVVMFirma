using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NowyTowarViewModel : JedenViewModel<Towary>
    {
       
        #region Constructor
        public NowyTowarViewModel()
            :base()
        {
            base.DisplayName = "Towar";
            item = new Towary();
        }
        #endregion Constructor
        #region Properties
        // dla każdego pola na interfejsie trzeba utworzyć propertisa zgodnie z poniższym wzorem
        public String Kod
        {
            get
            {
                return item.Kod;
            }
            set
            {
                if (item.Kod != value)
                {
                    item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }                
            }
        }
        public String Nazwa
        {
            get
            {
                return item.Nazwa;
            }
            set
            {
                if (item.Nazwa != value)
                {
                    item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
                }
            }
        }

        public decimal? StawkaVatZakupu
        {
            get
            {
                return item.StawkaVatZakupu;
            }
            set
            {
                if (item.StawkaVatZakupu != value)
                {
                    item.StawkaVatZakupu = value;
                    base.OnPropertyChanged(() => StawkaVatZakupu);
                }
            }
        }

        public decimal? StawkaVatSprzedazy
        {
            get
            {
                return item.StawkaVatSprzedazy;
            }
            set
            {
                if (item.StawkaVatSprzedazy != value)
                {
                    item.StawkaVatSprzedazy = value;
                    base.OnPropertyChanged(() => StawkaVatSprzedazy);
                }
            }
        }

        public decimal? Cena
        {
            get
            {
                return item.Cena;
            }
            set
            {
                if (item.Cena != value)
                {
                    item.Cena = value;
                    base.OnPropertyChanged(() => Cena);
                }
            }
        }

        public decimal? Marza
        {
            get
            {
                return item.Marza;
            }
            set
            {
                if (item.Marza != value)
                {
                    item.Marza = value;
                    base.OnPropertyChanged(() => Marza);
                }
            }
        }

        #endregion Properties
       
        #region Helpers
        public override void Save()
        {
            //najpierw dodajemy towar do lokalnej kolekcji towarów
            fakturyEntities.Towary.Add(item);
            //a następnie zapisujemy zmiany w bazie danych
            fakturyEntities.SaveChanges();
        }
    
        #endregion Helpers

    }
}

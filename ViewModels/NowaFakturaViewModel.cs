using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NowaFakturaViewModel : JedenWszystkieViewModel<Faktury, PozycjaFakturyForAllView>
    {
        #region Fields
        //to jest komenda która wywoła okno wyświetlające wszystkich kontrahentów
        private BaseCommand _ShowKontrahenci;
        #endregion Fields

        #region Konstruktor
        public NowaFakturaViewModel()
            :base()
        {
            base.DisplayName = "Faktura";
            base.DisplayListName = "Pozycje faktury";
            item = new Faktury();
            //  DataWystawienia = DateTime.Now;// - mi to nie dziala bo daty mam w stringach a nie DAteTime!!!!
            // DataWystawienia = DateTime.Now.AddDays(14);
            //ten messenger oczekuje na kontrahneta który przyjdzie do nowej faktury z okna wyświetlającego wszytskich kontrahentów
            //jezeli zlapiemy messegerem kontrahenta wybranego w oknie kontrahenci to wywołamy funkcję getWybranyKontrahent
            Messenger.Default.Register<KontrahenciForAllView>(this, getWybranyKontrahent);
            //to jest messenger który oczekuje na dodaną pozycję faktury w nowym oknie
            //messeneg jak zlapie pozycje faktory to wywoła metoda addPozycjeFaktury
            Messenger.Default.Register<PozycjeFaktury>(this, addPozycjaFaktury);
        }
        #endregion

        #region Command
        //ta komenda która wywoła okno wszyscy kontrahenci 
        //ta komenda do MainWindowViewModel wyśle messengerem komunikat KontrahenciShow 
        public ICommand ShowKontrahenci
        {
            get
            {
                if (_ShowKontrahenci == null)
                    _ShowKontrahenci = new BaseCommand(() => Messenger.Default.Send("KontrahenciShow"));

                return _ShowKontrahenci;
            }
        }
        #endregion

        #region Properties
        public string Numer
        {
            get
            {
                return item.Numer;
            }
            set
            {
                if (item.Numer != value)
                {
                    item.Numer = value;
                    base.OnPropertyChanged(() => Numer);
                }
            }
        }
        public string DataWystawienia
        {
            get
            {
                return item.DataWystawienia;
            }
            set
            {
                if (item.DataWystawienia != value)
                {
                    item.DataWystawienia = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public int? IdKontrahenta
        {
            get
            {
                return item.IdKontrahenta;
            }
            set
            {
                if (item.IdKontrahenta != value)
                {
                    item.IdKontrahenta = value;
                    base.OnPropertyChanged(() => IdKontrahenta);
                }
            }
        }
        private string _KontrahentNazwa;
        public string KontrahentNazwa
        {
            get
            {
                return _KontrahentNazwa;
            }
            set
            {
                if (_KontrahentNazwa != value)
                {
                    _KontrahentNazwa = value;
                    base.OnPropertyChanged(() => KontrahentNazwa);
                }
            }
        }
        private string _KontrahentNIP;
        public string KontrahentNIP
        {
            get
            {
                return _KontrahentNIP;
            }
            set
            {
                if (_KontrahentNIP != value)
                {
                    _KontrahentNIP = value;
                    base.OnPropertyChanged(() => KontrahentNIP);
                }
            }
        }
        private string _KontrahentAdres;
        public string KontrahentAdres
        {
            get
            {
                return _KontrahentAdres;
            }
            set
            {
                if (_KontrahentAdres != value)
                {
                    _KontrahentAdres = value;
                    base.OnPropertyChanged(() => KontrahentAdres);
                }
            }
        }
        //to jest properties obsługujący comboboxa z wyborem kontrahentów
        public IQueryable<ComboBoxKeyAndValue> KontrahenciComboBoxItems
        {
            get
            {
                return
                    (
                    //Zapytanie pobiera z bazy danych wszytskich kontrahentów 
                    //i zapisuje ich w comboBoxKeyAndValue
                    from kontrahent in fakturyEntities.Kontrahenci
                    select new ComboBoxKeyAndValue
                    {
                        Key=kontrahent.IdKontrahenta,
                        Value=kontrahent.Nazwa+" | "+kontrahent.Nip+" | "
                        + kontrahent.Adresy.Poczta+ " | "+ kontrahent.Adresy.Ulica + " "
                        +kontrahent.Adresy.NrDomu
                    }
                    ).ToList().AsQueryable();


            }
        }
        public string TerminPlatnosci
        {
            get
            {
                return item.TerminPlatnosci;
            }
            set
            {
                if (item.TerminPlatnosci != value)
                {
                    item.TerminPlatnosci = value;
                    base.OnPropertyChanged(() => TerminPlatnosci);
                }
            }
        }
        public int? IsSposobuPlatnosci
        {
            get
            {
                return item.IsSposobuPlatnosci;
            }
            set
            {
                if (item.IsSposobuPlatnosci != value)
                {
                    item.IsSposobuPlatnosci = value;
                    base.OnPropertyChanged(() => IsSposobuPlatnosci);
                }
            }
        }
        //to jest propertis ktory służy do wypełnienia combo boxa wyświetlającego do wyboru 
        //wszystkich sposobów płatności
        public IQueryable<IdSposobyPlatnosci> SposobyPlatnosciComboBoxItems
        {
            get
            {
                return
                    (
                    from platnosc in fakturyEntities.IdSposobyPlatnosci
                    select platnosc
                    ).ToList().AsQueryable();

                
            }
        }
        #endregion Properties
        #region Helpers


        //to jest metoda która pobiera z bazy wszytskie pozycje tej faktury
        public override void Load()
        {
            List = new ObservableCollection<PozycjaFakturyForAllView>
            (
                from pozycja in item.PozycjeFaktury //to daje to samo co WHERE, to pobiera pozycje tej faktury w której jesteśmy
                select new PozycjaFakturyForAllView
                
                {
                    TowarKod = pozycja.Towary.Kod,
                    TowarNazwa = pozycja.Towary.Nazwa,
                    Cena = pozycja.Cena,
                    Rabat = pozycja.Rabat,
                    Ilosc = pozycja.Ilosc
                }
                );
        }

        private void addPozycjaFaktury(PozycjeFaktury pozycjaFaktury)
        {
            //tworzymy nową pozycję faktury
            PozycjeFaktury nowa = new PozycjeFaktury();
            //wypelniamy jej dane
            nowa.IdTowaru = pozycjaFaktury.IdTowaru;
            nowa.Towary = fakturyEntities.Towary.Find(nowa.IdTowaru);
            nowa.Ilosc = pozycjaFaktury.Ilosc;
            nowa.Rabat = pozycjaFaktury.Rabat;
            nowa.Cena = pozycjaFaktury.Cena;
            //dodajemy do lokalnej kolekcji
            fakturyEntities.PozycjeFaktury.Add(nowa);
            //łączymy z aktualną fakturą
            item.PozycjeFaktury.Add(nowa);
            List.Add(
                new PozycjaFakturyForAllView()
                {
                    TowarKod = nowa.Towary.Kod,
                    TowarNazwa = nowa.Towary.Nazwa,
                    Cena = nowa.Cena,
                    Rabat = nowa.Rabat,
                    Ilosc = nowa.Ilosc
                }
             );
        }

        //ta metoda jest wywoływana przez messengera który złapie kontrahenta 
        private void getWybranyKontrahent(KontrahenciForAllView kontrahent)
        {
            //uzupełniamy dane kontrahenta w fakturze na bazie danych kontrahenta ktory przyjdzie z okna ze wszytskimi konttrahentami
            IdKontrahenta = kontrahent.IdKontrahenta;
            KontrahentNazwa = kontrahent.Nazwa;
            KontrahentNIP = kontrahent.Nip;
            KontrahentAdres = kontrahent.Adres;
        }

        public override void Save()
        {
       
            //najpierw dodajemy towar do lokalnej kolekcji towarów
            fakturyEntities.Faktury.Add(item);
            //a następnie zapisujemy zmiany w bazie danych
            fakturyEntities.SaveChanges();
        }

        #endregion Helpers
    }
}

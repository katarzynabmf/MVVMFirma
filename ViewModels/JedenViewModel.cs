using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public abstract class JedenViewModel<T>: WorkspaceViewModel //klasa ta działa na typie generycznym t pod który to typ w konkretnej klasie dziedziczącej
        //jest podstawiany konkretny typ dodawangeo rekordu
    {

        #region Fields
        protected FakturyEntities fakturyEntities; //sluzy do polaczenia z baza danych
        protected T item; // to jest towar który bedziemy dodawac
        private BaseCommand _SaveCommand; // to jest komenda to zapisu towaru
        #endregion Fields
        #region Constructor
        public JedenViewModel()
            :base()
        {
            fakturyEntities = new FakturyEntities(); // tworze polaczenie z baza danych
        }
        #endregion Constructor
       
        #region Command
        //to jest komenda która zostanie podpięta pod przycisk zapisz i ta komenda wywoła metodę saveAndClose
        //która będzie zdefiniowana niżej
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new BaseCommand(() => saveAndClose());
                }
                return _SaveCommand;
            }
        }
        #endregion Command

        #region Helpers
        public abstract void Save();
  
        private void saveAndClose()
        {
            //zapisujemy obiekt
            Save();
            base.OnRequestClose();
        }
        #endregion Helpers

    }
}


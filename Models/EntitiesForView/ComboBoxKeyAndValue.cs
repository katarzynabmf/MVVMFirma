using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace MVVMFirma.Models.EntitiesForView
{
    //to jest klasa pomocnicza do wyświetlania kluczy obcych w comboboxie
    public class ComboBoxKeyAndValue
    {
        //w key bedziemy przechowywac klucz tego co wybieramy
        public int Key { get; set; }
        //a w value bedziemy przechowywać wartość wyświetlaną w combo boxie 
        public string Value { get; set; }
    }
}

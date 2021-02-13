using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;

namespace MVVMFirma.Models.EntitiesForView
{
    public class KontrahenciForAllView
    {
        public int IdKontrahenta { get; set; }
        public string Kod { get; set; }
        public string Nip { get; set; }
        public string Nazwa { get; set; }
        public string RodzajNazwa { get; set; }
        public string StatusNazwa { get; set; }
        public string Adres { get; set; }
    }
}

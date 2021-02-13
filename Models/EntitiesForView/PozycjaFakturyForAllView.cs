using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;

namespace MVVMFirma.Models.EntitiesForView
{
    public class PozycjaFakturyForAllView
    {
        public int IdPozycjiFaktury { get; set; }
        public int IdFaktury { get; set; }
        public int IdTowaru { get; set; }
        public string TowarKod { get; set; }
        public string TowarNazwa { get; set; }
        public decimal? Ilosc { get; set; }
        public decimal? Cena { get; set; }
        public decimal? Rabat { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVVMFirma.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Faktury
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faktury()
        {
            this.PozycjeFaktury = new HashSet<PozycjeFaktury>();
        }
    
        public int IdFaktury { get; set; }
        public string Numer { get; set; }
        public string DataWystawienia { get; set; }
        public Nullable<int> IdKontrahenta { get; set; }
        public string TerminPlatnosci { get; set; }
        public Nullable<int> IsSposobuPlatnosci { get; set; }
        public Nullable<bool> CzyAktywny { get; set; }
    
        public virtual IdSposobyPlatnosci IdSposobyPlatnosci { get; set; }
        public virtual Kontrahenci Kontrahenci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PozycjeFaktury> PozycjeFaktury { get; set; }
    }
}

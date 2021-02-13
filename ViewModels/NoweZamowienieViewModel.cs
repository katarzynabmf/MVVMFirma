using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMFirma.ViewModels
{
    public class NoweZamowienieViewModel : WorkspaceViewModel//bo wszystkie VM zakładek dziedzcza po Work...
    {
        public NoweZamowienieViewModel()
        {
            base.DisplayName = "Zamowienie";
        }
    }
}
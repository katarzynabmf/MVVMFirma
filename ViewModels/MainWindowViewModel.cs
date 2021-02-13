using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Helper;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace MVVMFirma.ViewModels
{
    //to jest 
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        //to jest 
        private ReadOnlyCollection<CommandViewModel> _Commands;
        //to jest 
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands
        //
        //
        private BaseCommand _NowyTowarCommand;
        public ICommand NowyTowarCommand
        {
            get
            {
                if (_NowyTowarCommand == null)
                    _NowyTowarCommand = new BaseCommand(() => CreateView(new NowyTowarViewModel()));//
                return _NowyTowarCommand;
            }
        }
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            //to jest włączenie przy pomocy messengera nasłuchiwania na komunikat typu string.
            //jeżeli messenger złapie ten komunikat to wywoła metodę open która jest zdefiniowana w regionie helpers
            Messenger.Default.Register<string>(this, open);
            return new List<CommandViewModel>
            {
                //tu
                new CommandViewModel(
                    "Towary",
                    new BaseCommand(() => this.ShowAllTowar())),

                new CommandViewModel(
                    "Towar",
                    new BaseCommand(() => this.CreateView(new NowyTowarViewModel()))),
                
                new CommandViewModel(
                    "Kontrahenci",
                    new BaseCommand(() => this.ShowAllKontrahent())),

                 new CommandViewModel(
                    "Kontrahent",
                    new BaseCommand(() => this.CreateView(new NowyKontrahentViewModel()))),

                new CommandViewModel(
                    "Faktury",
                    new BaseCommand(() => this.ShowAllFaktura())),

                 new CommandViewModel(
                    "Faktura",
                    new BaseCommand(() => this.CreateView(new NowaFakturaViewModel()))),

                  new CommandViewModel(
                    "Zamówienia",
                    new BaseCommand(() => this.ShowAllZamowienia())),

                 new CommandViewModel(
                    "Zamówienie",
                    new BaseCommand(() => this.CreateView(new NoweZamowienieViewModel()))),
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers
        // to jest cmetoda Open, którą wywołuje messenger zdefiniowany w createCommands
        private void open(string name)
        {
            if (name == "FakturyAdd")
                this.CreateView(new NowaFakturaViewModel());
            if (name == "TowaryAdd")
                this.CreateView(new NowyTowarViewModel());
            if (name == "KontrahenciShow")
                this.ShowAllKontrahent();
            if (name == "Pozycje fakturyAdd")
                this.CreateView(new NowaPozycjaFakturyViewModel());

        }
        //
        //
        private void CreateView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllZamowienia()
        {
            WszystkieZamowieniaViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszystkieZamowieniaViewModel)
                as WszystkieZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllKontrahent()
        {
            WszyscyKontrahenciViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszyscyKontrahenciViewModel)
                as WszyscyKontrahenciViewModel;
            if (workspace == null)
            {
                workspace = new WszyscyKontrahenciViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllFaktura()
        {
            WszystkieFakturyViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszystkieFakturyViewModel)
                as WszystkieFakturyViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieFakturyViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        //
        private void ShowAllTowar()
        {
            WszystkieTowaryViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is WszystkieTowaryViewModel)
                as WszystkieTowaryViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieTowaryViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        //
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
    }
}

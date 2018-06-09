using Obst.ølCatalog.BLC;
using Obst.ølCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UI.Properties;

namespace Obst.ølCatalog.UI.ViewModels
{
    class PiwoListViewModel : ViewModelBase
    {
        public ObservableCollection<PiwoViewModel> Piwa { get; set; } = new ObservableCollection<PiwoViewModel>();
        private ListCollectionView _view;

        private RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand { get => _filterDataCommand; }

        private DataProvider _provider;

        public string FilterValue { get; set; }

        public PiwoListViewModel()
        {
            _provider = new DataProvider(_appSettings.mockName);
            OnPropertyChanged("Piwa");
            GetAllPiwa();
            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Piwa);
            _filterDataCommand = new RelayCommand(param => this.FilterData());
            _addNewPiwoCommand = new RelayCommand(param => this.AddNewPiwo());
            _saveChangesCommand = new RelayCommand(param => this.SaveChanges(), param => !EditingPiwo.HasErrors);

            EditingPiwo = Piwa[0];
            SelectedPiwo = EditingPiwo;
        }
        
        private Settings _appSettings = new Settings();

        private void GetAllPiwa()
        {
            List<IProducent> producenci = (List<IProducent>)_provider.Producenci;
            foreach (var piwo in _provider.Piwa)
            {
                Piwa.Add(new PiwoViewModel(piwo, producenci));
            }
        }

        private void FilterData()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                _view.Filter = null;
            }
            else
            {
                _view.Filter = (c) => ((PiwoViewModel)c).Nazwa.Contains(FilterValue);
            }
        }

        private PiwoViewModel _selectedPiwo;
        public PiwoViewModel SelectedPiwo
        {
            get => _selectedPiwo;
            set
            {
                _selectedPiwo = value;
                EditingPiwo = value;
                OnPropertyChanged(nameof(SelectedPiwo));
            }
        }

        private PiwoViewModel _editingPiwo;
        public PiwoViewModel EditingPiwo
        {
            get => _editingPiwo;
            set
            {
                _editingPiwo = value;
                OnPropertyChanged(nameof(EditingPiwo));
            }
        }

        private void SaveChanges()
        {
            if (Piwa.Contains(EditingPiwo))
            {
                _provider.SavePiwo(EditingPiwo.Piwo, Piwa.IndexOf(EditingPiwo));
            }
            else
            {
                _provider.SavePiwo(EditingPiwo.Piwo);
                Piwa.Add(EditingPiwo);
            }
            SelectedPiwo = EditingPiwo;
            OnPropertyChanged(nameof(Piwa));
        }

        private RelayCommand _saveChangesCommand;
        public RelayCommand SaveChangesCommand
        {
            get => _saveChangesCommand;
        }

        private void AddNewPiwo()
        {
            IPiwo newPiwo = _provider.AddPiwo();
            EditingPiwo = new PiwoViewModel(newPiwo, (List<IProducent>)_provider.Producenci);
            EditingPiwo.Validate();
        }

        private RelayCommand _addNewPiwoCommand;
        public RelayCommand AddNewPiwoCommand
        {
            get => _addNewPiwoCommand;
        }
    }
}

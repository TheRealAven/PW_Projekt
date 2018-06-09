using Obst.ølCatalog.BLC;
using Obst.ølCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using UI.Properties;

namespace Obst.ølCatalog.UI.ViewModels
{
    class ProducentListViewModel: ViewModelBase
    {
        public ObservableCollection<ProducentViewModel> Producenci { get; set; } = new ObservableCollection<ProducentViewModel>();
        private ListCollectionView _view;

        private RelayCommand _filterDataCommand;
        public RelayCommand FilterDataCommand { get => _filterDataCommand; }

        public string FilterValue { get; set; }

        private DataProvider _provider;

        public ProducentListViewModel()
        {
            _provider = new DataProvider(_appSettings.mockName);
            OnPropertyChanged("Producenci");
            GetAllProducenci();
            _view = (ListCollectionView)CollectionViewSource.GetDefaultView(Producenci);
            _filterDataCommand = new RelayCommand(param => this.FilterData());
            _addNewProducentCommand = new RelayCommand(param => this.AddNewProducent());
            _saveChangesCommand = new RelayCommand(param => this.SaveChanges(), param => !EditingProducent.HasErrors);

            EditingProducent = Producenci[0];
            SelectedProducent = EditingProducent;
        }

        private Settings _appSettings = new Settings();

        private void GetAllProducenci()
        {
            foreach (var producent in _provider.Producenci)
            {
                Producenci.Add(new ProducentViewModel(producent));
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
                _view.Filter = (c) => ((ProducentViewModel)c).Nazwa.Contains(FilterValue);
            }
        }

        private ProducentViewModel _selectedProducent;
        public ProducentViewModel SelectedProducent
        {
            get => _selectedProducent;
            set
            {
                _selectedProducent = value;
                EditingProducent = value;
                OnPropertyChanged(nameof(SelectedProducent));
            }
        }

        private ProducentViewModel _editingProducent;
        public ProducentViewModel EditingProducent
        {
            get => _editingProducent;
            set
            {
                _editingProducent = value;
                OnPropertyChanged(nameof(EditingProducent));
            }
        }

        private void SaveChanges()
        {
            if (Producenci.Contains(EditingProducent))
            {
                _provider.SaveProducent(EditingProducent.Producent, Producenci.IndexOf(EditingProducent));
            }
            else
            {
                _provider.SaveProducent(EditingProducent.Producent);
                Producenci.Add(EditingProducent);
            }
            SelectedProducent = EditingProducent;
            OnPropertyChanged(nameof(Producenci));
        }

        private RelayCommand _saveChangesCommand;
        public RelayCommand SaveChangesCommand
        {
            get => _saveChangesCommand;
        }

        private void AddNewProducent()
        {
            IProducent newProducent = _provider.AddProducent();
            EditingProducent = new ProducentViewModel(newProducent);
            EditingProducent.Validate();
        }

        private RelayCommand _addNewProducentCommand;
        public RelayCommand AddNewProducentCommand
        {
            get => _addNewProducentCommand;
        }
    }
}

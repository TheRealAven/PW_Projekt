using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Obst.ølCatalog.Interfaces;
using System.Collections.ObjectModel;
using Obst.ølCatalog.BLC;
using UI.Properties;

namespace Obst.ølCatalog.UI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Settings _appSettings = new Settings();
        private ObservableCollection<IPiwo> _piwoDataList;
        public ObservableCollection<IPiwo> PiwoDataList
        {
            get { return _piwoDataList; }
            set { _piwoDataList = value; }
        }
        private ObservableCollection<IProducent> _producentDataList;
        public ObservableCollection<IProducent> ProducentDataList
        {
            get { return _producentDataList; }
            set { _producentDataList = value; }
        }

        public MainWindow()
        {
            DataProvider provider = new DataProvider(_appSettings.mockName);
            PiwoDataList = new ObservableCollection<IPiwo>(provider.Piwa);
            ProducentDataList = new ObservableCollection<IProducent>(provider.Producenci);
            InitializeComponent();
        }
    }
}

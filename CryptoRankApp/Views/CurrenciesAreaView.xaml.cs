using Prism.Regions;
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

namespace CryptoRankApp.Views
{
    /// <summary>
    /// Interaction logic for CurrenciesAreaView.xaml
    /// </summary>
    public partial class CurrenciesAreaView : UserControl
    {
        public CurrenciesAreaView(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion(RegionNames.CurrenciesContentRegion, typeof(TopCurrenciesView));
        }
    }
}

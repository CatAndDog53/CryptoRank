using Prism.Ioc;
using System.Windows;
using CryptoRankApp.Views;

namespace CryptoRankApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CurrenciesAreaView>();
            containerRegistry.RegisterForNavigation<TopCurrenciesView>();
            containerRegistry.RegisterForNavigation<DetailsView>();
            containerRegistry.RegisterForNavigation<ConverterView>();
        }
    }

}

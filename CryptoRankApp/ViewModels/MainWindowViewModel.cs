using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace CryptoRankApp.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private string _title = "Crypto Rank";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}

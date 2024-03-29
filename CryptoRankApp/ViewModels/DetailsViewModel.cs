﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace CryptoRankApp.ViewModels
{
    public class DetailsViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public DetailsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.CurrenciesContentRegion, navigatePath);
        }
    }
}

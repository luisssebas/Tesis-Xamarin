using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    public class PantallaInicialPageViewModel : INavigatedAware
    {
        INavigationService _navigationService;

        public PantallaInicialPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}

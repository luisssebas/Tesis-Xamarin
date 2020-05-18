using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class PantallaInicialPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        public DelegateCommand AccederCmd { get; set; }

        public PantallaInicialPageViewModel(INavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;

            AccederCmd = new DelegateCommand(AccederEjecutar);
        }

        private async void AccederEjecutar()
        {
            await _navigationService.NavigateAsync("LoginPage");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}

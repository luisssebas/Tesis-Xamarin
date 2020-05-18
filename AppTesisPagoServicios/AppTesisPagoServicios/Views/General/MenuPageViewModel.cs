using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Services;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    public class MenuPageViewModel : IPageLifecycleAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;

        public ObservableCollection<MenuMD> ListaMenu { get; set; }
        public DelegateCommand<MenuMD> NavegarCmd { get; private set; }

        public MenuPageViewModel(INavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            var datos = DatosGlobales.Obtiene();
            if (datos.EsAdmin)
                ListaMenu = MenuServicio.DevuelveMenuAdministrador();
            else
                ListaMenu = MenuServicio.DevuelveMenuUsuario();

            NavegarCmd = new DelegateCommand<MenuMD>(NavegarEjecutar);
        }

        private async void NavegarEjecutar(MenuMD menu)
        {
            if (menu.Vista != null)
            {
                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/" + menu.Vista);
            }

        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}

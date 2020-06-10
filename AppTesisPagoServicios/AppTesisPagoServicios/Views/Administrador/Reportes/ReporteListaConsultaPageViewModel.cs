using Acr.UserDialogs;
using AppTesisPagoServicios.Models.DTO;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class ReporteListaConsultaPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand<ConsultaMD> ConsultaSeleccionarCmd { get; set; }

        public ObservableCollection<ConsultaMD> Consultas { get; set; }

        public ReporteListaConsultaPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            ConsultaSeleccionarCmd = new DelegateCommand<ConsultaMD>(ConsultaSeleccionarEjecutar);

            Consultas = new ObservableCollection<ConsultaMD>();
        }

        private async void ConsultaSeleccionarEjecutar(ConsultaMD item)
        {
            try
            {
                NavigationParameters parameters = new NavigationParameters()
                {
                    {"Consulta", item }
                };

                await _navigationService.NavigateAsync("ReporteConsultaPage", parameters);
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Consultas"))
                Consultas = parameters["Consultas"] as ObservableCollection<ConsultaMD>;
        }
    }
}

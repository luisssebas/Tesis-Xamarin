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
    public class ReporteListaPagoPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand<PagoCompletoMD> PagoSeleccionarCmd { get; set; }

        public CatalogoMD TipoServicio { get; set; }
        public ServicioMD Servicio { get; set; }

        public ObservableCollection<PagoCompletoMD> Pagos { get; set; }

        public ReporteListaPagoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            PagoSeleccionarCmd = new DelegateCommand<PagoCompletoMD>(PagoSeleccionarEjecutar);

            Pagos = new ObservableCollection<PagoCompletoMD>();

            TipoServicio = new CatalogoMD();
            Servicio = new ServicioMD();
        }

        private async void PagoSeleccionarEjecutar(PagoCompletoMD item)
        {
            try
            {
                NavigationParameters parameters = new NavigationParameters()
                {
                    {"Pago", item },
                    {"TipoServicio", TipoServicio },
                    {"Servicio", Servicio },
                };

                await _navigationService.NavigateAsync("ReportePagoPage", parameters);
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
            if (parameters.ContainsKey("Pagos"))
                Pagos = parameters["Pagos"] as ObservableCollection<PagoCompletoMD>;

            if (parameters.ContainsKey("TipoServicio"))
                TipoServicio = parameters["TipoServicio"] as CatalogoMD;

            if (parameters.ContainsKey("Servicio"))
                Servicio = parameters["Servicio"] as ServicioMD;
        }
    }
}

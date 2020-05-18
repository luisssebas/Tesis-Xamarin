using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class InformacionPagoPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public InformacionPagoMD InformacionPago { get; set; }

        private ConsultaMS _consulta;
        private List<RubrosMS> _rubros;
        private string _TipoServicio;
        private string _servicio;
        private string _contrapartida;

        public InformacionPagoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            InformacionPago = new InformacionPagoMD();
        }

        public async void ObtenerDatos()
        {
            try
            {
                var parametrizaciones = await _servicioUsuario.ListaParametrizacion();
                var parametrizacion = parametrizaciones.FirstOrDefault();
                double valorPago = 0;

                foreach (var item in _rubros)
                {
                    valorPago += item.ValorAPagar;
                }

                InformacionPago = new InformacionPagoMD()
                {
                    Comision = parametrizacion.Comision,
                    Contrapartida = _contrapartida,
                    Servicio = _servicio,
                    TipoServicio = _TipoServicio,
                    ValorAPagar = valorPago,
                    ValorTotal = ValorPago.DevuelveFormateado(valorPago + parametrizacion.Comision, 20, Color.FromHex("233979"), FontAttributes.Bold)
                };
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
            if (parameters.ContainsKey("Consulta"))
            {
                _consulta = parameters["Consulta"] as ConsultaMS;
                ObtenerDatos();
            }

            if (parameters.ContainsKey("Contrapartida"))
                _contrapartida = parameters["Contrapartida"].ToString();

            if (parameters.ContainsKey("Servicio"))
                _servicio = parameters["Servicio"].ToString();

            if (parameters.ContainsKey("TipoServicio"))
                _TipoServicio = parameters["TipoServicio"].ToString();

            if (parameters.ContainsKey("TipoServicio"))
                _rubros = parameters["Rubros"] as List<RubrosMS>;
        }
    }
}

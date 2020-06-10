using Acr.UserDialogs;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaSalida;
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
    public class PagoCompletoPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand ListoCmd { get; set; }

        public ServicioMD Servicio { get; set; }
        public PagoCompletoMD PagoCompleto { get; set; }
        public InformacionPagoMD InformacionPago { get; set; }

        public ObservableCollection<RubrosMD> Rubros { get; set; }

        public int AlturaRubros { get; set; }

        private ConsultaMS _consulta;
        private PagoMS _pago;
        private List<RubrosMS> _rubros;
        private string _TipoServicio;
        private string _contrapartida;
        private string PaypalID;

        public PagoCompletoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            PagoCompleto = new PagoCompletoMD();
            ListoCmd = new DelegateCommand(ListoEjecutar);

            Rubros = new ObservableCollection<RubrosMD>();
        }

        private async void ListoEjecutar()
        {
            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/SeleccionTipoServiciosPage");
        }

        public void ObtenerDatos()
        {
            try
            {
                PagoCompleto = new PagoCompletoMD()
                {
                    Comision = InformacionPago.Comision,
                    Contrapartida = _contrapartida,
                    Documento = "Documento N° " + _pago.Documento,
                    TipoServicio = _TipoServicio,
                    Identificacion = _consulta.Identificacion,
                    Nombre = _consulta.Nombre,
                    ValorPagado = InformacionPago.ValorTotal,
                    PaypalID = PaypalID
                };

                Rubros.Clear();

                foreach (var item in _rubros)
                {
                    RubrosMD rubro = new RubrosMD()
                    {
                        Periodo = item.Periodo,
                        Prioridad = item.Prioridad,
                        ValorAPagar = item.ValorAPagar,
                        Descripcion = item.Descripcion
                    };

                    Rubros.Add(rubro);
                }

                AlturaRubros = Rubros.Count * 200;
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
            if (parameters.ContainsKey("PaypalID"))
                PaypalID = parameters["PaypalID"].ToString();
            
            if (parameters.ContainsKey("Contrapartida"))
                _contrapartida = parameters["Contrapartida"].ToString();

            if (parameters.ContainsKey("Servicio"))
                Servicio = parameters["Servicio"] as ServicioMD;

            if (parameters.ContainsKey("TipoServicio"))
                _TipoServicio = parameters["TipoServicio"].ToString();

            if (parameters.ContainsKey("Rubros"))
                _rubros = parameters["Rubros"] as List<RubrosMS>;

            if (parameters.ContainsKey("Pago"))
                _pago = parameters["Pago"] as PagoMS;

            if (parameters.ContainsKey("InformacionPago"))
                InformacionPago = parameters["InformacionPago"] as InformacionPagoMD;

            if (parameters.ContainsKey("Consulta"))
            {
                _consulta = parameters["Consulta"] as ConsultaMS;
                ObtenerDatos();
            }
        }
    }
}

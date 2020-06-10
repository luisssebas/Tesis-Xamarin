using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public DelegateCommand PagarCmd { get; set; }

        public int AlturaRubros { get; set; }

        public InformacionPagoMD InformacionPago { get; set; }
        public ServicioMD Servicio { get; set; }

        public ObservableCollection<RubrosMD> Rubros { get; set; }

        private ConsultaMS _consulta;
        private List<RubrosMS> _rubros;
        private string _TipoServicio;
        private string _contrapartida;
        private double _valorAPagar;

        public InformacionPagoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            InformacionPago = new InformacionPagoMD();

            PagarCmd = new DelegateCommand(PagarEjecutar);

            Rubros = new ObservableCollection<RubrosMD>();

        }

        private async void PagarEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    var respuesta = await _servicioUsuario.EnviarOTP(DatosGlobales.Obtiene().Username);

                    if (respuesta)
                    {
                        NavigationParameters parameters = new NavigationParameters()
                        {
                            {"Consulta", _consulta},
                            {"Contrapartida", _contrapartida},
                            {"Servicio", Servicio},
                            {"TipoServicio", _TipoServicio},
                            {"Rubros", _rubros},
                            {"InformacionPago", InformacionPago}
                        };

                        await _navigationService.NavigateAsync("OTPPage", parameters);
                    }
                }

            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        public async void ObtenerDatos()
        {
            try
            {
                var parametrizaciones = await _servicioUsuario.ListaParametrizacion();
                var parametrizacion = parametrizaciones.FirstOrDefault();

                var nombre = _consulta.Nombre.Split(' ');

                InformacionPago = new InformacionPagoMD()
                {
                    Comision = parametrizacion.Comision,
                    Contrapartida = _contrapartida,
                    Servicio = Servicio.Nombre,
                    TipoServicio = _TipoServicio,
                    ValorAPagar = _valorAPagar,
                    ValorTotal = ValorPago.DevuelveFormateado(_valorAPagar + parametrizacion.Comision, 30, Color.FromHex("#233979"), FontAttributes.Bold),
                    ValorTotalDato = _valorAPagar + parametrizacion.Comision,
                    Identificacion = _consulta.Identificacion,
                    Nombre = nombre[0] + " " + nombre[2]
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
            if (parameters.ContainsKey("ValorApagar"))
                _valorAPagar = Convert.ToDouble(parameters["ValorApagar"]);

            if (parameters.ContainsKey("Consulta"))
            {
                _consulta = parameters["Consulta"] as ConsultaMS;
                ObtenerDatos();
            }

            if (parameters.ContainsKey("Contrapartida"))
                _contrapartida = parameters["Contrapartida"].ToString();

            

            if (parameters.ContainsKey("Servicio"))
                Servicio = parameters["Servicio"] as ServicioMD;

            if (parameters.ContainsKey("TipoServicio"))
                _TipoServicio = parameters["TipoServicio"].ToString();

            if (parameters.ContainsKey("Rubros"))
                _rubros = parameters["Rubros"] as List<RubrosMS>;
        }
    }
}

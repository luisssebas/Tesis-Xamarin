using Acr.UserDialogs;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class ElegirPagoPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand PagarCmd { get; set; }

        public ObservableCollection<RubrosMD> Rubros { get; set; }

        public InformacionPagoMD InformacionPago { get; set; }
        public ServicioMD Servicio { get; set; }

        private ConsultaMS _consulta;
        private string _contrapartida;
        private string _tipoServicio;
        private List<RubrosMS> _rubros;
        private double _total = 0;
        private int _count = 0;

        public ElegirPagoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Rubros = new ObservableCollection<RubrosMD>();

            PagarCmd = new DelegateCommand(PagarEjecutar);
        }

        private bool ValidarSeleccion()
        {
            var tempRubros = Rubros.Where(u => u.Seleccionado == true).OrderBy(u=> u.Prioridad).ToList();
            bool validado = false;
            int count = 0;

            foreach (var item in tempRubros)
            {
                count++;
                if (item.Prioridad == count)
                {
                    var dato = _consulta.Rubros.Where(u => u.Prioridad == count).FirstOrDefault().ValorAPagar;
                    _total += _consulta.Rubros.Where(u => u.Prioridad == count).FirstOrDefault().ValorAPagar;
                    validado = true;
                }
                else
                {
                    validado = false;
                    return validado;
                }

            }
            _count = count;

            return validado;
        }

        private async void PagarEjecutar()
        {
            try
            {
                bool correcto = ValidarSeleccion();
                List<RubrosMS> rubros = new List<RubrosMS>();

                double valor = 0;

                if (_total != 0 && correcto)
                {
                    foreach (var item in Rubros.Where(u => u.Seleccionado == true))
                    {
                        valor += item.ValorAPagar;
                        RubrosMS rubro = new RubrosMS()
                        {
                            ValorAPagar = item.ValorAPagar,
                            Prioridad = item.Prioridad,
                            Periodo = item.Periodo,
                            Descripcion = item.Descripcion
                        };

                        rubros.Add(rubro);
                    }

                    NavigationParameters parameters = new NavigationParameters()
                    {
                        {"Consulta", _consulta},
                        {"Contrapartida", _contrapartida},
                        {"TipoServicio", _tipoServicio},
                        {"Servicio", Servicio},
                        {"Rubros", rubros},
                        {"ValorApagar", valor}
                    };

                    await _navigationService.NavigateAsync("InformacionPagoPage", parameters);
                }
                else
                {
                    _total = 0;
                    _count = 0;
                    await _userDialogs.AlertAsync("Seleccione las cuotas en orden de prioridad");
                }
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
                _consulta = parameters["Consulta"] as ConsultaMS;

            if (parameters.ContainsKey("Contrapartida"))
                _contrapartida = parameters["Contrapartida"].ToString();

            if (parameters.ContainsKey("Servicio"))
                Servicio = parameters["Servicio"] as ServicioMD;

            if (parameters.ContainsKey("TipoServicio"))
                _tipoServicio = parameters["TipoServicio"].ToString();

            if (parameters.ContainsKey("Rubros"))
                _rubros = parameters["Rubros"] as List<RubrosMS>;

            if (parameters.ContainsKey("InformacionPago"))
                InformacionPago = parameters["InformacionPago"] as InformacionPagoMD;

            Rubros.Clear();

            foreach (var item in _rubros)
            {
                RubrosMD rubro = new RubrosMD()
                {
                    Periodo = item.Periodo,
                    Prioridad = item.Prioridad,
                    ValorAPagar = item.ValorAPagar,
                    Seleccionado = false,
                    Descripcion = item.Descripcion
                };

                Rubros.Add(rubro);
            }
        }
    }
}

using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class ReporteBusquedaPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand BuscarConsultaCmd { get; set; }

        public CatalogoMD TipoServicio
        {
            get => tipoServicioSeleccionado;
            set
            {
                if (tipoServicioSeleccionado != value)
                {
                    tipoServicioSeleccionado = value;
                    TipoServicioSeleccionadoEjecutar(tipoServicioSeleccionado);
                }
            }
        }
        public ServicioMD Servicio { get; set; }
        public PagoCompletoMD Pago { get; set; }

        public string Reporte { get; set; }

        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<ServicioMD> Servicios { get; set; }
        public ObservableCollection<string> Reportes { get; set; }
        public ObservableCollection<ConsultaMD> Consultas { get; set; }
        public ObservableCollection<PagoCompletoMD> Pagos { get; set; }
        public ObservableCollection<RubrosMS> Rubros { get; set; }

        public DateTime DiaMaximo { get; set; }
        public DateTime DiaInicio { get; set; }
        public DateTime DiaFin { get; set; }

        private List<ServicioMS> _servicios;
        private CatalogoMD tipoServicioSeleccionado;

        public ReporteBusquedaPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            Servicios = new ObservableCollection<ServicioMD>();
            TiposServicios = new ObservableCollection<CatalogoMD>();
            Consultas = new ObservableCollection<ConsultaMD>();
            Pagos = new ObservableCollection<PagoCompletoMD>();
            Rubros = new ObservableCollection<RubrosMS>();
            Reportes = new ObservableCollection<string>();

            TipoServicio = new CatalogoMD();

            DiaMaximo = DateTime.Now;
            DiaInicio = DateTime.Now;
            DiaFin = DateTime.Now;

            BuscarConsultaCmd = new DelegateCommand(BuscarConsultaEjecutar);

            Reportes.Add("Consulta");
            Reportes.Add("Pago");
        }

        private void TipoServicioSeleccionadoEjecutar(CatalogoMD tipoServicioSeleccionado)
        {
            try
            {
                if (tipoServicioSeleccionado != null)
                {
                    if (tipoServicioSeleccionado.Id != 0)
                    {
                        Servicios.Clear();

                        foreach (var item in _servicios)
                        {
                            if (tipoServicioSeleccionado.Id == item.TipoServicioId)
                            {
                                ServicioMD dato = new ServicioMD()
                                {
                                    ServicioId = item.ServicioId,
                                    Activo = item.EstaActivo,
                                    ComisionRubro = item.ComisionRubro,
                                    LongitudReferencia = item.LongitudReferencia,
                                    Nombre = item.Nombre,
                                    ServicioConsulta = item.ServicioConsulta,
                                    ServicioPago = item.ServicioPago,
                                    TipoServicio = TiposServicios.Where(u => u.Id == item.TipoServicioId).FirstOrDefault(),
                                };
                                Servicios.Add(dato);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

       

        private async void BuscarConsultaEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    if (TipoServicio.Id != 0)
                    {
                        ConsultaFechaME mensajeEntrada = new ConsultaFechaME()
                        {
                            FechaFin = DiaFin,
                            FechaInicio = DiaInicio
                        };

                        if (Reporte == "Consulta")
                        {
                            var mensajeSalida = await _servicioUsuario.ListaConsultasFechas(mensajeEntrada);

                            Consultas.Clear();

                            foreach (var item in mensajeSalida)
                            {
                                if(item.ServicioId == Servicio.ServicioId)
                                {
                                    ConsultaMD dato = new ConsultaMD()
                                    {
                                        ServicioId = item.ServicioId,
                                        ConsultaId = item.ConsultaId,
                                        Descripcion = item.Descripcion,
                                        FechaConsulta = item.FechaConsulta.ToString("ddd dd MMM yyyy hh:mm", new CultureInfo("es-ES")),
                                        Referencia = item.Referencia,
                                        Rubros = item.Rubros,
                                        UsuarioId = item.UsuarioId,
                                        Nombre = item.Nombre,
                                        Fecha = item.FechaConsulta
                                    };
                                    Consultas.Add(dato);
                                }
                            }

                            NavigationParameters parameters = new NavigationParameters()
                            {
                                {"Consultas", Consultas }
                            };

                            await _navigationService.NavigateAsync("ReporteListaConsultaPage", parameters);
                        }
                        else
                        {
                            var mensajeSalida = await _servicioUsuario.ListaPagoFechas(mensajeEntrada);
                            var parametrizaciones = await _servicioUsuario.ListaParametrizacion();
                            var parametrizacion = parametrizaciones.FirstOrDefault();
                            var consultas = await _servicioUsuario.ListaConsultas();
                            Pagos.Clear();
                            Rubros.Clear();

                            foreach (var item in mensajeSalida)
                            {
                                var consulta = consultas.Where(u => u.ConsultaId == item.ConsultaId).FirstOrDefault();

                                PagoCompletoMD dato = new PagoCompletoMD()
                                {
                                    Identificacion = DatosGlobales.Obtiene().Identificacion,
                                    Comision = parametrizacion.Comision,
                                    Contrapartida = consulta.Referencia,
                                    Documento = item.Documento,
                                    TipoServicio = TipoServicio.Nombre,
                                    ValorPagado = ValorPago.DevuelveFormateado(item.ValorPagado, 30, Color.FromHex("#233979"), FontAttributes.Bold),
                                    Nombre = consulta.Nombre,
                                    PaypalID = item.IdPasarelaPago,
                                    Fecha = consulta.FechaConsulta.ToString("ddd dd MMM yyyy hh:mm", new CultureInfo("es-ES")),
                                    Rubros = item.Rubros
                                };
                                Pagos.Add(dato);
                            }

                            NavigationParameters parameters = new NavigationParameters()
                            {
                                {"Pagos", Pagos },
                                {"TipoServicio", TipoServicio },
                                {"Servicio", Servicio },
                            };

                            await _navigationService.NavigateAsync("ReporteListaPagoPage", parameters);
                        }
                    }
                    else
                    {
                        await _userDialogs.AlertAsync("Seleccione un tipo de servicio");
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
                using (_userDialogs.Loading("Cargando"))
                {
                    var tipoServicio = await _servicioUsuario.ListaTipoServicios();
                    var consultas = await _servicioUsuario.ListaConsultas();
                    _servicios = await _servicioUsuario.ListaServicios();
                    TiposServicios.Clear();
                    Servicios.Clear();

                    foreach (var item in tipoServicio)
                    {
                        CatalogoMD dato = new CatalogoMD()
                        {
                            Id = item.TipoServicioId,
                            EstaActivo = item.EstaActivo,
                            Nombre = item.Nombre
                        };
                        TiposServicios.Add(dato);
                    }

                    foreach (var item in _servicios)
                    {
                        ServicioMD dato = new ServicioMD()
                        {
                            Nombre = item.Nombre,
                            ServicioId = item.ServicioId,
                            EstaActivo = item.EstaActivo ? "Activado" : "Desactivado",
                            ComisionRubro = item.ComisionRubro,
                            LongitudReferencia = item.LongitudReferencia,
                            ServicioConsulta = item.ServicioConsulta,
                            ServicioPago = item.ServicioPago,
                            Activo = item.EstaActivo
                        };
                        Servicios.Add(dato);
                    }
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
            if(TiposServicios.Count == 0)
                ObtenerDatos();
        }
    }
}

using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
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
    public class RegistroServiciosPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand<ServicioUsuarioMD> ServicioSeleccionarCmd { get; set; }
        public DelegateCommand AgregarServicioCmd { get; set; }

        public ObservableCollection<ServicioUsuarioMD> Servicios { get; set; }

        public RegistroServiciosPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Servicios = new ObservableCollection<ServicioUsuarioMD>();

            ServicioSeleccionarCmd = new DelegateCommand<ServicioUsuarioMD>(ServicioSeleccionarEjecutar);
            AgregarServicioCmd = new DelegateCommand(AgregarServicioEjecutar);
        }

        private async void AgregarServicioEjecutar()
        {
            try
            {
                await _navigationService.NavigateAsync("RegistrarServicioPage");
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void ServicioSeleccionarEjecutar(ServicioUsuarioMD item)
        {
            try
            {
                //NavigationParameters parameters = new NavigationParameters()
                //{
                //    {"ServicioUsuario", item}
                //};

                //await _navigationService.NavigateAsync("RegistrarServicioPage", parameters);

                using (_userDialogs.Loading("Cargando"))
                {
                    ConsultaME consulta = new ConsultaME()
                    {
                        Descripcion = "Consulta Movil",
                        FechaConsulta = DateTime.Now,
                        Referencia = item.Contrapartida,
                        ServicioId = item.Servicio.ServicioId,
                        UsuarioId = DatosGlobales.Obtiene().UsuarioId,
                    };

                    var mensajeSalida = await _servicioUsuario.RealizaConsulta(consulta);

                    if (mensajeSalida.UsuarioId > 0)
                    {
                        double valorAPagar = 0;

                        foreach (var item2 in mensajeSalida.Rubros)
                            valorAPagar += item2.ValorAPagar;

                        NavigationParameters parameters = new NavigationParameters()
                        {
                            {"Consulta", mensajeSalida},
                            {"Contrapartida", item.Contrapartida},
                            {"TipoServicio", item.Servicio.TipoServicio.Nombre},
                            {"Servicio", item.Servicio},
                            {"Rubros", mensajeSalida.Rubros},
                            {"ValorApagar", valorAPagar}
                        };

                        if (item.Servicio.TipoPago.Id == 1)
                            await _navigationService.NavigateAsync("ElegirPagoPage", parameters);
                        else
                            await _navigationService.NavigateAsync("InformacionPagoPage", parameters);
                    }
                    else
                    {
                        await _userDialogs.AlertAsync("No se encuentran valores a pagar");
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
                if(Servicios.Count == 0)
                {
                    var mensajeEntrada = await _servicioUsuario.ListaServiciosUsuarios(DatosGlobales.Obtiene().UsuarioId);
                    var mensajeServicio = await _servicioUsuario.ListaServicios();
                    var mensajeTipoServicios = await _servicioUsuario.ListaTipoServicios();
                    var mensajeTipoPagos = await _servicioUsuario.ListaTipoPago();
                    var mensajeTipoReferencias = await _servicioUsuario.ListaTipoReferencia();

                    Servicios.Clear();

                    foreach (var item in mensajeEntrada)
                    {
                        var servicio = mensajeServicio.Where(u => u.ServicioId == item.ServicioId).FirstOrDefault();
                        var tipoServicio = mensajeTipoServicios.Where(u => u.TipoServicioId == servicio.TipoServicioId).FirstOrDefault();
                        var tipoPago = mensajeTipoServicios.Where(u => u.TipoServicioId == servicio.TipoServicioId).FirstOrDefault();
                        var tipoReferencia = mensajeTipoServicios.Where(u => u.TipoServicioId == servicio.TipoServicioId).FirstOrDefault();

                        ServicioUsuarioMD dato = new ServicioUsuarioMD()
                        {
                            UsuarioId = item.UsuarioId,
                            Contrapartida = item.Contrapartida,
                            TipoServicio = new CatalogoMD() { EstaActivo = tipoServicio.EstaActivo, Nombre = tipoServicio.Nombre, Id = tipoServicio.TipoServicioId },
                            Servicio = new ServicioMD()
                            {
                                Nombre = servicio.Nombre,
                                ServicioId = servicio.ServicioId,
                                EstaActivo = servicio.EstaActivo ? "Activado" : "Desactivado",
                                ComisionRubro = servicio.ComisionRubro,
                                LongitudReferencia = servicio.LongitudReferencia,
                                ServicioConsulta = servicio.ServicioConsulta,
                                ServicioPago = servicio.ServicioPago,
                                TipoPago = new CatalogoMD() { EstaActivo = tipoPago.EstaActivo, Nombre = tipoPago.Nombre, Id = tipoPago.TipoServicioId },
                                TipoReferencia = new CatalogoMD() { EstaActivo = tipoReferencia.EstaActivo, Nombre = tipoReferencia.Nombre, Id = tipoReferencia.TipoServicioId },
                                TipoServicio = new CatalogoMD() { EstaActivo = tipoServicio.EstaActivo, Nombre = tipoServicio.Nombre, Id = tipoServicio.TipoServicioId },
                                Activo = servicio.EstaActivo
                            },
                            ServicioId = item.ServicioId,
                            ServicioUsuarioId = item.ServicioUsuarioId
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
            ObtenerDatos();
        }
    }
}

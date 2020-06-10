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
    public class RegistrarServicioPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand GuardarServicioUsuarioCmd { get; set; }
        public DelegateCommand EliminarServicioUsuarioCmd { get; set; }
        public DelegateCommand PagarServicioUsuarioCmd { get; set; }

        public ServicioMD Servicio { get; set; }
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

        public ServicioUsuarioMD ServicioUsuario { get; set; }

        public ObservableCollection<ServicioMD> Servicios { get; set; }
        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }

        public bool MostrarEliminar { get; set; }
        public bool MostrarPagar { get; set; }


        private ServicioUsuarioMD _servicio;
        private CatalogoMD tipoServicioSeleccionado;

        public RegistrarServicioPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            ServicioUsuario = new ServicioUsuarioMD();

            TiposServicios = new ObservableCollection<CatalogoMD>();
            Servicios = new ObservableCollection<ServicioMD>();

            TipoServicio = new CatalogoMD();
            Servicio = new ServicioMD();

            MostrarEliminar = false;
            MostrarPagar = false;

            GuardarServicioUsuarioCmd = new DelegateCommand(GuardarServicioUsuarioEjecutar);
            EliminarServicioUsuarioCmd = new DelegateCommand(EliminarServicioUsuarioEjecutar);
            PagarServicioUsuarioCmd = new DelegateCommand(PagarServicioUsuarioEjecutar);
        }

        private async void PagarServicioUsuarioEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    ConsultaME consulta = new ConsultaME()
                    {
                        Descripcion = "Consulta Movil",
                        FechaConsulta = DateTime.Now,
                        Referencia = ServicioUsuario.Contrapartida,
                        ServicioId = _servicio.Servicio.ServicioId,
                        UsuarioId = DatosGlobales.Obtiene().UsuarioId,
                    };

                    var mensajeSalida = await _servicioUsuario.RealizaConsulta(consulta);

                    if (mensajeSalida.Rubros.Count > 0)
                    {
                        NavigationParameters parameters = new NavigationParameters()
                        {
                            {"Consulta", mensajeSalida},
                            {"Contrapartida", ServicioUsuario.Contrapartida},
                            {"TipoServicio", _servicio.Servicio.TipoServicio.Nombre},
                            {"Servicio", _servicio.Servicio},
                            {"Rubros", mensajeSalida.Rubros}
                        };

                        if (mensajeSalida.Rubros.Count == 1)
                        {
                            await _navigationService.NavigateAsync("InformacionPagoPage", parameters);
                        }
                        else
                        {
                            await _navigationService.NavigateAsync("ElegirPagoPage", parameters);
                        }
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

        private async void EliminarServicioUsuarioEjecutar()
        {
            try
            {
                var respuesta = await _userDialogs.ConfirmAsync("¿Está seguro de eliminar el servicio registrado?", "Eliminar", "Si", "No");

                if (respuesta)
                {
                    using (_userDialogs.Loading("Cargando"))
                    {
                        var mensajeSalida = await _servicioUsuario.EliminarTipoReferencia(ServicioUsuario.ServicioUsuarioId);
                        await _navigationService.NavigateAsync("/MenuPage/NavigationPage/RegistroServiciosPage");
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void GuardarServicioUsuarioEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    ServicioUsuarioValidator servicioUsuarioValidator = new ServicioUsuarioValidator();
                    FluentValidation.Results.ValidationResult validationResult = servicioUsuarioValidator.Validate(ServicioUsuario);

                    if (!validationResult.IsValid)
                        _userDialogs.Alert(ErroresValidacion.Despliega(validationResult));

                    else
                    {
                        ServicioUsuarioME mensajeEntrada = new ServicioUsuarioME()
                        {
                            EstaActivo = true,
                            ServicioId = ServicioUsuario.Servicio.ServicioId,
                            Contrapartida = ServicioUsuario.Contrapartida,
                            UsuarioId = DatosGlobales.Obtiene().UsuarioId,
                            ServicioUsuarioId = ServicioUsuario.ServicioUsuarioId
                        };

                        if (mensajeEntrada.ServicioUsuarioId == 0)
                        {
                            var mensajeSalida = await _servicioUsuario.GuardarServicioUsuario(mensajeEntrada);

                            if (mensajeSalida)
                            {
                                await _userDialogs.AlertAsync("Se ha registrado un nuevo servicio");
                                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/RegistroServiciosPage");
                            }
                            else
                                await _userDialogs.AlertAsync("No se ha podido agregar");
                        }
                        else
                        {
                            var mensajeSalida = await _servicioUsuario.ActualizarServicioUsuario(mensajeEntrada);

                            if (mensajeSalida)
                            {
                                await _userDialogs.AlertAsync("Se ha actualizado el servicio");
                                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/RegistroServiciosPage");
                            }
                            else
                                await _userDialogs.AlertAsync("No se ha podido actualizar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void TipoServicioSeleccionadoEjecutar(CatalogoMD tipoServicioSeleccionado)
        {
            try
            {
                if(tipoServicioSeleccionado != null)
                {
                    ServicioUsuario.TipoServicio = tipoServicioSeleccionado;
                    var _servicios = await _servicioUsuario.ListaServicios();
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
                                ServicioPago = item.ServicioPago,
                                ServicioConsulta = item.ServicioConsulta,
                                TipoServicio = TiposServicios.Where(u => u.Id == item.TipoServicioId).FirstOrDefault(),
                            };
                            Servicios.Add(dato);
                        }
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
                var mensajeServicio = await _servicioUsuario.ListaServicios();
                var mensajeTipoServicios = await _servicioUsuario.ListaTipoServicios();

                TiposServicios.Clear();

                foreach (var item in mensajeTipoServicios)
                {
                    if (item.EstaActivo)
                    {
                        CatalogoMD catalogo = new CatalogoMD()
                        {
                            Nombre = item.Nombre,
                            Id = item.TipoServicioId,
                            EstaActivo = item.EstaActivo
                        };
                        TiposServicios.Add(catalogo);
                    }
                }

                if(_servicio != null)
                {
                    MostrarEliminar = true;
                    MostrarPagar = true;

                    ServicioUsuario = new ServicioUsuarioMD()
                    {
                        UsuarioId = _servicio.UsuarioId,
                        Contrapartida = _servicio.Contrapartida,
                        Servicio = _servicio.Servicio,
                        ServicioId = _servicio.ServicioId,
                        TipoServicio = _servicio.TipoServicio,
                        ServicioUsuarioId = _servicio.ServicioUsuarioId
                    };

                    TipoServicio = new CatalogoMD()
                    {
                        Nombre = _servicio.Servicio.Nombre,
                        EstaActivo = _servicio.Servicio.Activo,
                        Id = _servicio.ServicioId
                    };

                    Servicio = new ServicioMD()
                    {
                        TipoServicio = _servicio.Servicio.TipoServicio,
                        TipoReferencia = _servicio.Servicio.TipoReferencia,
                        ComisionRubro = _servicio.Servicio.ComisionRubro,
                        EstaActivo = _servicio.Servicio.EstaActivo,
                        LongitudReferencia = _servicio.Servicio.LongitudReferencia,
                        Nombre = _servicio.Servicio.Nombre,
                        ServicioConsulta = _servicio.Servicio.ServicioConsulta,
                        ServicioPago = _servicio.Servicio.ServicioPago,
                        ServicioId = _servicio.Servicio.ServicioId,
                        TipoPago = _servicio.Servicio.TipoPago,
                        Activo = _servicio.Servicio.Activo
                    };
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
            if (parameters.ContainsKey("ServicioUsuario"))
                _servicio = parameters["ServicioUsuario"] as ServicioUsuarioMD;
            
            ObtenerDatos();

        }
    }
}

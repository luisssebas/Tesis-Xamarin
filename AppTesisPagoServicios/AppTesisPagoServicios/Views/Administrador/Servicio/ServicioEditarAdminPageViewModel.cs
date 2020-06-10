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
using static AppTesisPagoServicios.Models.DTO.ServicioMD;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class ServicioEditarAdminPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand RegresarServicioCmd { get; set; }
        public DelegateCommand GuardarServicioCmd { get; set; }
        public DelegateCommand EliminarServicioCmd { get; set; }

        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<CatalogoMD> TiposReferencias { get; set; }
        public ObservableCollection<CatalogoMD> TiposPagos { get; set; }
        public ObservableCollection<ServicioMD> Servicios { get; set; }

        public ServicioMD Servicio { get; set; }

        public ServicioEditarAdminPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            TiposReferencias = new ObservableCollection<CatalogoMD>();
            TiposPagos = new ObservableCollection<CatalogoMD>();

            Servicio = new ServicioMD();

            RegresarServicioCmd = new DelegateCommand(RegresarServicioEjecutar);
            GuardarServicioCmd = new DelegateCommand(GuardarServicioEjecutar);
            EliminarServicioCmd = new DelegateCommand(EliminarServicioEjecutar);
    }

        private async void EliminarServicioEjecutar()
        {
            try
            {
                var respuesta = await _userDialogs.ConfirmAsync("¿Está seguro de eliminar el servicio?", "Eliminar", "Si", "No");

                if (respuesta)
                {
                    using (_userDialogs.Loading("Cargando"))
                    {
                        var mensajeSalida = await _servicioUsuario.EliminarServicio(Servicio.ServicioId);
                        if (mensajeSalida)
                        {
                            await _userDialogs.AlertAsync("Se ha eliminado el servicio");
                            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/ServicioAdminPage");
                        }
                        else
                            await _userDialogs.AlertAsync("No se ha podido eliminar el servicio");
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void GuardarServicioEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    ServicioValidator servicioValidador = new ServicioValidator();
                    FluentValidation.Results.ValidationResult validationResult = servicioValidador.Validate(Servicio);

                    if (!validationResult.IsValid)
                        _userDialogs.Alert(ErroresValidacion.Despliega(validationResult));
                    else
                    {
                        ServicioME mensajeEntrada = new ServicioME()
                        {
                            EstaActivo = Servicio.Activo,
                            Nombre = Servicio.Nombre,
                            ComisionRubro = Servicio.ComisionRubro,
                            LongitudReferencia = Servicio.LongitudReferencia,
                            ServicioConsulta = Servicio.ServicioConsulta,
                            ServicioPago = Servicio.ServicioPago,
                            TipoPagoId = Servicio.TipoPago.Id,
                            TipoReferenciaId = Servicio.TipoReferencia.Id,
                            TipoServicioId = Servicio.TipoServicio.Id,
                            ServicioId = Servicio.ServicioId
                        };

                        if (mensajeEntrada.ServicioId == 0)
                        {
                            var mensajeSalida = await _servicioUsuario.GuardarServicio(mensajeEntrada);

                            if (mensajeSalida)
                            {
                                await _userDialogs.AlertAsync("Se ha agregado un nuevo servicio");
                                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/ServicioAdminPage");
                            }
                            else
                                await _userDialogs.AlertAsync("No se ha podido agregar");
                        }
                        else
                        {
                            var mensajeSalida = await _servicioUsuario.ActualizarServicio(mensajeEntrada);

                            if (mensajeSalida)
                            {
                                await _userDialogs.AlertAsync("Se ha actualizado el servicio");
                                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/ServicioAdminPage");
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

        private async void RegresarServicioEjecutar()
        {
            await _navigationService.NavigateAsync("ServicioAdminPage");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("TiposPagos"))
                TiposPagos = parameters["TiposPagos"] as ObservableCollection<CatalogoMD>;
            if (parameters.ContainsKey("TiposServicios"))
                TiposServicios = parameters["TiposServicios"] as ObservableCollection<CatalogoMD>;
            if (parameters.ContainsKey("TiposReferencias"))
                TiposReferencias = parameters["TiposReferencias"] as ObservableCollection<CatalogoMD>;
            if (parameters.ContainsKey("Servicio"))
            {
                var servicio = parameters["Servicio"] as ServicioMD;

                Servicio = new ServicioMD()
                {
                    TipoServicio = TiposServicios.Where(u => u.Id == servicio.TipoServicio.Id).FirstOrDefault(),
                    TipoReferencia = TiposReferencias.Where(u => u.Id == servicio.TipoReferencia.Id).FirstOrDefault(),
                    ComisionRubro = servicio.ComisionRubro,
                    EstaActivo = servicio.EstaActivo,
                    LongitudReferencia = servicio.LongitudReferencia,
                    Nombre = servicio.Nombre,
                    ServicioConsulta = servicio.ServicioConsulta,
                    ServicioPago = servicio.ServicioPago,
                    ServicioId = servicio.ServicioId,
                    TipoPago = TiposPagos.Where(u=> u.Id == servicio.TipoPago.Id).FirstOrDefault(),
                    Activo = servicio.Activo
                };
            }
        }
    }
}

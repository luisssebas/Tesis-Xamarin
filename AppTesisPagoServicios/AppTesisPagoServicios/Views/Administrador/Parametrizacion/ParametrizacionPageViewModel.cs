using Acr.UserDialogs;
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
    public class ParametrizacionPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand GuardarParametrizacionCmd { get; set; }

        public ParametrizacionMD Parametrizacion { get; set; }

        public ParametrizacionPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Parametrizacion = new ParametrizacionMD();
            GuardarParametrizacionCmd = new DelegateCommand(GuardarParametrizacionEjecutar);
        }

        private async void GuardarParametrizacionEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    ParametrizacionME mensajeEntrada = new ParametrizacionME()
                    {
                        Comision = Parametrizacion.Comision,
                        ContraseniaCorreo = Parametrizacion.ContraseniaCorreo,
                        Correo = Parametrizacion.Correo,
                        MaxIntentosLogin = Parametrizacion.MaxIntentosLogin,
                        MensajeNuevaCuenta = Parametrizacion.MensajeNuevaCuenta,
                        MensajePagoServicio = Parametrizacion.MensajePagoServicio,
                        MensajeRecuperacionContrasenia = Parametrizacion.MensajeRecuperacionContrasenia,
                        ParametrizacionId = Parametrizacion.ParametrizacionId
                    };

                    if (mensajeEntrada.ParametrizacionId == 0)
                    {
                        var mensajeSalida = await _servicioUsuario.GuardarParametrizacion(mensajeEntrada);

                        if (mensajeSalida)
                            await _userDialogs.AlertAsync("Se han agregado las parametrizaciones");
                        else
                            await _userDialogs.AlertAsync("No se ha podido agregar");
                    }
                    else
                    {
                        var mensajeSalida = await _servicioUsuario.ActualizarParametrizacion(mensajeEntrada);

                        if (mensajeSalida)
                            await _userDialogs.AlertAsync("Se ha actualizado las parametrizaciones");
                        else
                            await _userDialogs.AlertAsync("No se ha podido actualizar");
                    }
                    ObtenerDatos();
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void ObtenerDatos()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    var mensajeSalida = await _servicioUsuario.ListaParametrizacion();

                    if (mensajeSalida.Count == 1)
                    {
                        Parametrizacion = new ParametrizacionMD()
                        {
                            Comision = mensajeSalida.FirstOrDefault().Comision,
                            ContraseniaCorreo = mensajeSalida.FirstOrDefault().ContraseniaCorreo,
                            Correo = mensajeSalida.FirstOrDefault().Correo,
                            MaxIntentosLogin = mensajeSalida.FirstOrDefault().MaxIntentosLogin,
                            MensajeNuevaCuenta = mensajeSalida.FirstOrDefault().MensajeNuevaCuenta,
                            MensajePagoServicio = mensajeSalida.FirstOrDefault().MensajePagoServicio,
                            MensajeRecuperacionContrasenia = mensajeSalida.FirstOrDefault().MensajeRecuperacionContrasenia,
                            ParametrizacionId = mensajeSalida.FirstOrDefault().ParametrizacionId
                        };
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

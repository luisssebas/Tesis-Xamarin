using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using Plugin.Fingerprint;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class LoginPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand IngresarCmd { get; set; }
        public DelegateCommand RegistrarCmd { get; set; }

        public LoginMD Login { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Login = new LoginMD();

            IngresarCmd = new DelegateCommand(IngresarEjecutar);
            RegistrarCmd = new DelegateCommand(RegistrarEjecutar);

        }

        private async void IngresarHuellaEjecutar()
        {
            if (DatosGlobales.Obtiene().UsuarioId != 0)
            {
                var result = await CrossFingerprint.Current.AuthenticateAsync("Ingrese su huella para ingresar a One Pay");
                if (result.Authenticated)
                {
                    using (_userDialogs.Loading("Cargando"))
                    {
                        if (DatosGlobales.Obtiene().EsAdmin)
                            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/AdministradorPage");
                        else
                            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/SeleccionTipoServiciosPage");
                    }
                }
                else
                {
                    await _userDialogs.AlertAsync("No se puede leer la huella", "Huella Incorrecta", "Ok");
                }
            }
        }

        private async void RegistrarEjecutar()
        {
            await _navigationService.NavigateAsync("RegistroUsuarioPage");
        }

        public async void IngresarEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    LoginME mensajeEntrada = new LoginME()
                    {
                        Usuario = Login.Usuario,
                        Contrasenia = Login.Contrasenia
                    };

                    LoginMS mensajeSalida = await _servicioUsuario.Login(mensajeEntrada);

                    if (mensajeSalida.Mensaje == "Ok")
                    {
                        DatosGlobales datos = new DatosGlobales(mensajeSalida.Username, mensajeSalida.UsuarioId, mensajeSalida.Identificacion, mensajeSalida.Nombre, mensajeSalida.Correo, mensajeSalida.EsAdmin);
                        datos.Adiciona();

                        if (mensajeSalida.EsAdmin)
                            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/AdministradorPage");
                        else
                            await _navigationService.NavigateAsync("/MenuPage/NavigationPage/SeleccionTipoServiciosPage");
                    }
                    else
                        await _userDialogs.AlertAsync(mensajeSalida.Mensaje);
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
            IngresarHuellaEjecutar();
        }
    }
}

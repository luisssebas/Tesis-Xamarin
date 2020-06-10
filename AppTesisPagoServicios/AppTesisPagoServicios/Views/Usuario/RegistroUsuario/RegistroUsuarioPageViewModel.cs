using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class RegistroUsuarioPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand RegistrarUsuarioCmd { get; set; }

        public UsuarioMD Usuario { get; set; }

        public RegistroUsuarioPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Usuario = new UsuarioMD();

            RegistrarUsuarioCmd = new DelegateCommand(RegistrarUsuarioEjecutar);
        }

        private async void RegistrarUsuarioEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    UsuarioValidator usuarioValidador = new UsuarioValidator();
                    FluentValidation.Results.ValidationResult validationResult = usuarioValidador.Validate(Usuario);

                    if (!validationResult.IsValid)
                        _userDialogs.Alert(ErroresValidacion.Despliega(validationResult));
                    else
                    {
                        if(Usuario.Contrasenia == Usuario.ComprobacionContrasenia)
                        {
                            UsuarioME usuario = new UsuarioME()
                            {
                                Bloqueado = false,
                                Contrasenia = Usuario.Contrasenia,
                                Correo = Usuario.Correo,
                                EsAdmin = false,
                                EstaActivo = true,
                                FechaActualizacion = DateTime.Now,
                                FechaCreacion = DateTime.Now,
                                Identificacion = Usuario.Identificacion,
                                Nombre = Usuario.Nombre,
                                Username = Usuario.Usuario
                            };

                            var mensajeSalida = await _servicioUsuario.RegistrarUsuario(usuario);

                            if (mensajeSalida)
                            {
                                await _userDialogs.AlertAsync("Se registrado el usuario satisfactoriamente");
                                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/PantallaInicialPage/LoginPage");
                            }
                            else
                                await _userDialogs.AlertAsync("No se ha podido registrar el usuario");
                        }
                        else
                        {
                            await _userDialogs.AlertAsync("Las contraseñas no coinciden");
                        }
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
        }
    }
}

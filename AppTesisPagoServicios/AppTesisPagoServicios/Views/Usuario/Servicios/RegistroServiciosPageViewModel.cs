using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTesisPagoServicios.Views.Usuario.Servicios
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class RegistroServiciosPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand ServicioSeleccionarCmd { get; set; }
        public DelegateCommand AgregarServicioCmd { get; set; }

        public ObservableCollection<ServicioUsuarioMD> Servicios { get; set; }

        public RegistroServiciosPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Servicios = new ObservableCollection<ServicioUsuarioMD>();

            ServicioSeleccionarCmd = new DelegateCommand(ServicioSeleccionarEjecutar);
            AgregarServicioCmd = new DelegateCommand(AgregarServicioEjecutar);
        }

        private void AgregarServicioEjecutar()
        {
        }

        private void ServicioSeleccionarEjecutar()
        {
        }

        public async void ObtenerDatos()
        {
            try
            {
                var mensajeEntrada = await _servicioUsuario.ListaServiciosUsuarios(DatosGlobales.Obtiene().UsuarioId);

                Servicios.Clear();

                foreach (var item in mensajeEntrada)
                {
                    ServicioUsuarioMD dato = new ServicioUsuarioMD()
                    {
                        UsuarioId = item.UsuarioId,
                        Contrapartida = item.Contrapartida,
                        NombreServicio = item.NombreServicio,
                        ServicioId = item.ServicioId,
                        TipoServicio = item.TipoServicio
                    };
                    Servicios.Add(dato);
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

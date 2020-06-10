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
    public class SeleccionTipoServiciosPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand SeleccionarServicioLuzCmd { get; set; }
        public DelegateCommand SeleccionarServicioAguaCmd { get; set; }
        public DelegateCommand SeleccionarServicioTelefonoCmd { get; set; }
        public DelegateCommand SeleccionarServicioCmd { get; set; }

        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<ServicioMD> Servicios { get; set; }

        private List<ServicioMS> _servicios;

        public SeleccionTipoServiciosPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            Servicios = new ObservableCollection<ServicioMD>();

            SeleccionarServicioLuzCmd = new DelegateCommand(SeleccionarServicioLuzEjecutar);
            SeleccionarServicioAguaCmd = new DelegateCommand(SeleccionarServicioAguaEjecutar);
            SeleccionarServicioTelefonoCmd = new DelegateCommand(SeleccionarServicioTelefonoEjecutar);
            SeleccionarServicioCmd = new DelegateCommand(SeleccionarServicioEjecutar);
        }

        private async void SeleccionarServicioTelefonoEjecutar()
        {
            try
            {
                var tipoServicio = TiposServicios.Where(u => u.Nombre.Contains("Teléfono")).FirstOrDefault();
                NavigationParameters parameters = new NavigationParameters();

                if (tipoServicio == null)
                {
                    tipoServicio = TiposServicios.Where(u => u.Nombre.Contains("Telefono")).FirstOrDefault();
                    parameters.Add("TipoServicio", tipoServicio);
                    parameters.Add("Servicios", _servicios.Where(u => u.TipoServicioId == tipoServicio.Id).ToList());

                    await _navigationService.NavigateAsync("ListaServiciosPage", parameters);
                }
                else
                {
                    parameters.Add("TipoServicio", tipoServicio);
                    parameters.Add("Servicios", _servicios.Where(u => u.TipoServicioId == tipoServicio.Id).ToList());

                    await _navigationService.NavigateAsync("ListaServiciosPage", parameters);
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void SeleccionarServicioAguaEjecutar()
        {
            try
            {
                var tipoServicio = TiposServicios.Where(u => u.Nombre.Contains("Agua")).FirstOrDefault();
                NavigationParameters parameters = new NavigationParameters
                {
                    { "TipoServicio", tipoServicio },
                    { "Servicios", _servicios.Where(u=> u.TipoServicioId == tipoServicio.Id).ToList() }
                };

                await _navigationService.NavigateAsync("ListaServiciosPage", parameters);
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void SeleccionarServicioLuzEjecutar()
        {
            try
            {
                var tipoServicio = TiposServicios.Where(u => u.Nombre.Contains("Luz")).FirstOrDefault();
                NavigationParameters parameters = new NavigationParameters
                {
                    { "Servicios", _servicios.Where(u=> u.TipoServicioId == tipoServicio.Id).ToList() },
                    { "TipoServicio", tipoServicio }
                };

                await _navigationService.NavigateAsync("ListaServiciosPage", parameters);
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void SeleccionarServicioEjecutar()
        {
            try
            {
                NavigationParameters parameters = new NavigationParameters
                {
                    { "Servicios", _servicios }
                };

                await _navigationService.NavigateAsync("ListaServiciosPage", parameters);
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
                var mensajeSalidaTipoServicios = await _servicioUsuario.ListaTipoServicios();
                _servicios = await _servicioUsuario.ListaServicios();
                TiposServicios.Clear();
                Servicios.Clear();

                foreach (var item in mensajeSalidaTipoServicios)
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
                        ServicioId = item.TipoServicioId,
                        Activo = item.EstaActivo,
                        ComisionRubro = item.ComisionRubro,
                        LongitudReferencia = item.LongitudReferencia,
                        Nombre = item.Nombre,
                        ServicioPago = item.ServicioPago,
                        ServicioConsulta = item.ServicioConsulta
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

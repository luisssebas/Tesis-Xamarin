using Acr.UserDialogs;
using AppTesisPagoServicios.Models.DTO;
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
    public class ServicioAdminPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand<ServicioMD> ServicioSeleccionarCmd { get; set; }
        public DelegateCommand AgregarServicioCmd { get; set; }

        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<CatalogoMD> TiposReferencias { get; set; }
        public ObservableCollection<CatalogoMD> TiposPagos { get; set; }
        public ObservableCollection<ServicioMD> Servicios { get; set; }

        public ServicioAdminPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            TiposReferencias = new ObservableCollection<CatalogoMD>();
            TiposPagos = new ObservableCollection<CatalogoMD>();
            Servicios = new ObservableCollection<ServicioMD>();

            AgregarServicioCmd = new DelegateCommand(AgregarServicioEjecutar);
            ServicioSeleccionarCmd = new DelegateCommand<ServicioMD>(ServicioSeleccionarEjecutar);
        }

        private async void ServicioSeleccionarEjecutar(ServicioMD item)
        {
            NavigationParameters parameters = new NavigationParameters()
            {
                {"Servicio", item },
                {"TiposPagos", TiposPagos },
                {"TiposServicios", TiposServicios },
                {"TiposReferencias", TiposReferencias }
            };

            await _navigationService.NavigateAsync("ServicioEditarAdminPage", parameters);
        }

        private void AgregarServicioEjecutar()
        {
            NavigationParameters parameters = new NavigationParameters()
            {
                {"Servicio", new ServicioMD() },
                {"TiposPagos", TiposPagos },
                {"TiposServicios", TiposServicios },
                {"TiposReferencias", TiposReferencias }
            };

            _navigationService.NavigateAsync("ServicioEditarAdminPage", parameters);
        }

        private async void ObtenerDatos()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    var mensajeSalidaServicios = await _servicioUsuario.ListaServicios();
                    var mensajeSalidaTipoPago = await _servicioUsuario.ListaTipoPago();
                    var mensajeSalidaTipoReferencia = await _servicioUsuario.ListaTipoReferencia();
                    var mensajeSalidaTipoServicio = await _servicioUsuario.ListaTipoServicios();

                    Servicios.Clear();
                    TiposPagos.Clear();
                    TiposServicios.Clear();
                    TiposReferencias.Clear();

                    foreach (var item in mensajeSalidaTipoPago)
                    {
                        CatalogoMD catalogo = new CatalogoMD()
                        {
                            Nombre = item.Nombre,
                            Id = item.TipoPagoId,
                            EstaActivo = item.EstaActivo
                        };
                        TiposPagos.Add(catalogo);
                    }

                    foreach (var item in mensajeSalidaTipoServicio)
                    {
                        CatalogoMD catalogo = new CatalogoMD()
                        {
                            Nombre = item.Nombre,
                            Id = item.TipoServicioId,
                            EstaActivo = item.EstaActivo
                        };
                        TiposServicios.Add(catalogo);
                    }

                    foreach (var item in mensajeSalidaTipoReferencia)
                    {
                        CatalogoMD catalogo = new CatalogoMD()
                        {
                            Nombre = item.Nombre,
                            Id = item.TipoReferenciaId,
                            EstaActivo = item.EstaActivo
                        };
                        TiposReferencias.Add(catalogo);
                    }
                    
                    foreach (var item in mensajeSalidaServicios)
                    {
                        if (item.EstaActivo)
                        {
                            ServicioMD catalogo = new ServicioMD()
                            {
                                Nombre = item.Nombre,
                                ServicioId = item.ServicioId,
                                EstaActivo = item.EstaActivo ? "Activado" : "Desactivado",
                                ComisionRubro = item.ComisionRubro,
                                LongitudReferencia = item.LongitudReferencia,
                                ServicioHttp = item.ServicioHttp,
                                TipoPago = TiposPagos.Where(u => u.Id == item.TipoPagoId).FirstOrDefault(),
                                TipoReferencia = TiposReferencias.Where(u => u.Id == item.TipoReferenciaId).FirstOrDefault(),
                                TipoServicio = TiposServicios.Where(u => u.Id == item.TipoServicioId).FirstOrDefault(),
                                Activo = item.EstaActivo
                            };
                            Servicios.Add(catalogo);
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
            ObtenerDatos();
        }
    }
}

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
    public class ReporteConsultaPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand BuscarConsultaCmd { get; set; }

        public CatalogoMD TipoServicio { get; set; }

        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<ConsultaMD> Consultas { get; set; }
        public ObservableCollection<ConsultaMD> BuscarConsultas { get; set; }

        private List<ServicioMS> _servicios;

        public ReporteConsultaPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            Consultas = new ObservableCollection<ConsultaMD>();
            BuscarConsultas = new ObservableCollection<ConsultaMD>();

            TipoServicio = new CatalogoMD();

            BuscarConsultaCmd = new DelegateCommand(BuscarConsultaEjecutar);
        }

        private void BuscarConsultaEjecutar()
        {
            try
            {
                var servicio = _servicios.Where(u => u.TipoServicioId == TipoServicio.Id).FirstOrDefault();
                BuscarConsultas = new ObservableCollection<ConsultaMD>(Consultas.Where(u => u.ServicioId == servicio.ServicioId).ToList());
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
                var tipoServicio = await _servicioUsuario.ListaTipoServicios();
                var consultas = await _servicioUsuario.ListaConsultas();
                _servicios = await _servicioUsuario.ListaServicios();
                TiposServicios.Clear();
                Consultas.Clear();

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

                foreach (var item in consultas)
                {
                    ConsultaMD dato = new ConsultaMD()
                    {
                        ServicioId = item.ServicioId,
                        ConsultaId = item.ConsultaId,
                        Descripcion = item.Descripcion,
                        FechaConsulta = item.FechaConsulta,
                        Referencia = item.Referencia,
                        Rubros = item.Rubros,
                        UsuarioId = item.UsuarioId
                    };
                    Consultas.Add(dato);
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

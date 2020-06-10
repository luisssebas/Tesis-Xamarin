using Acr.UserDialogs;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ConsultaMD Consulta { get; set; }
        public CatalogoMD TipoServicio { get; set; }
        public ServicioMD Servicio { get; set; }

        private ConsultaMD _consulta;
        private List<ServicioMS> _servicios;

        public ReporteConsultaPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

        }

        public async void ObtenerDatos()
        {
            try
            {
                _servicios = await _servicioUsuario.ListaServicios();
                var tipoServicios = await _servicioUsuario.ListaTipoServicios();

                Consulta = new ConsultaMD()
                {
                    ServicioId = _consulta.ServicioId,
                    ConsultaId = _consulta.ConsultaId,
                    Descripcion = _consulta.Descripcion,
                    FechaConsulta = _consulta.FechaConsulta,
                    Referencia = _consulta.Referencia,
                    Rubros = _consulta.Rubros,
                    UsuarioId = _consulta.UsuarioId,
                    Nombre = _consulta.Nombre
                };

                var servicio = _servicios.Where(u => u.ServicioId == Consulta.ServicioId).FirstOrDefault();
                var tipoServicio = tipoServicios.Where(u => u.TipoServicioId == servicio.TipoServicioId).FirstOrDefault();

                TipoServicio = new CatalogoMD()
                {
                     Id = tipoServicio.TipoServicioId,
                     EstaActivo = tipoServicio.EstaActivo,
                     Nombre = tipoServicio.Nombre
                };

                Servicio = new ServicioMD()
                {
                    TipoServicio = TipoServicio,
                    ComisionRubro = servicio.ComisionRubro,
                    LongitudReferencia = servicio.LongitudReferencia,
                    Nombre = servicio.Nombre,
                    ServicioConsulta = servicio.ServicioConsulta,
                    ServicioPago = servicio.ServicioPago,
                    ServicioId = servicio.ServicioId,
                    Activo = servicio.EstaActivo
                };
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
            if (parameters.ContainsKey("Consulta"))
                _consulta = parameters["Consulta"] as ConsultaMD;
            ObtenerDatos();
        }
    }
}

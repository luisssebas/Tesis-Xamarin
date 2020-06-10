using Acr.UserDialogs;
using AppTesisPagoServicios.Models.DTO;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class ReportePagoPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public PagoCompletoMD PagoCompleto { get; set; }
        public CatalogoMD TipoServicio { get; set; }
        public ServicioMD Servicio { get; set; }

        public ObservableCollection<RubrosMD> Rubros { get; set; }

        public int AlturaRubros { get; set; }

        private PagoCompletoMD _pago;
        private CatalogoMD _tipoServicio;
        private ServicioMD _servicio;

        public ReportePagoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            PagoCompleto = new PagoCompletoMD();
            TipoServicio = new CatalogoMD();
            Servicio = new ServicioMD();

            Rubros = new ObservableCollection<RubrosMD>();
        }

        public void ObtenerDatos()
        {
            try
            {
                PagoCompleto = new PagoCompletoMD()
                {
                    Identificacion = _pago.Identificacion,
                    Comision = _pago.Comision,
                    Contrapartida = _pago.Contrapartida,
                    Documento = _pago.Documento,
                    TipoServicio = _pago.Nombre,
                    ValorPagado = _pago.ValorPagado,
                    Nombre = _pago.Nombre,
                    PaypalID = _pago.PaypalID,
                    Fecha = _pago.Fecha,
                    Rubros = _pago.Rubros
                };

                Rubros.Clear();

                foreach (var item in PagoCompleto.Rubros)
                {
                    RubrosMD rubro = new RubrosMD()
                    {
                        Periodo = item.Periodo,
                        Prioridad = item.Prioridad,
                        ValorAPagar = item.ValorAPagar,
                        Descripcion = item.Descripcion
                    };

                    Rubros.Add(rubro);
                }

                AlturaRubros = Rubros.Count * 200;

                TipoServicio = new CatalogoMD()
                {
                    Id = _tipoServicio.Id,
                    EstaActivo = _tipoServicio.EstaActivo,
                    Nombre = _tipoServicio.Nombre
                };

                Servicio = new ServicioMD()
                {
                    TipoServicio = TipoServicio,
                    ComisionRubro = _servicio.ComisionRubro,
                    LongitudReferencia = _servicio.LongitudReferencia,
                    Nombre = _servicio.Nombre,
                    ServicioConsulta = _servicio.ServicioConsulta,
                    ServicioPago = _servicio.ServicioPago,
                    ServicioId = _servicio.ServicioId,
                    Activo = _servicio.Activo
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
            if (parameters.ContainsKey("Pago"))
                _pago = parameters["Pago"] as PagoCompletoMD;

            if (parameters.ContainsKey("TipoServicio"))
                _tipoServicio = parameters["TipoServicio"] as CatalogoMD;

            if (parameters.ContainsKey("Servicio"))
                _servicio = parameters["Servicio"] as ServicioMD;

            ObtenerDatos();
        }
    }
}

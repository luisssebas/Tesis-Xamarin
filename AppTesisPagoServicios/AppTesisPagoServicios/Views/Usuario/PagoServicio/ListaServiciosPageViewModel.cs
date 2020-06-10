﻿using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class ListaServiciosPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public ServicioMD Servicio
        {
            get => servicioSeleccionado;
            set
            {
                if (servicioSeleccionado != value)
                {
                    servicioSeleccionado = value;
                    ServicioSeleccionadoEjecutar(servicioSeleccionado);
                }
            }
        }
        
        public CatalogoMD TipoServicio
        {
            get => tipoServicioSeleccionado;
            set
            {
                if (tipoServicioSeleccionado != value)
                {
                    tipoServicioSeleccionado = value;
                    TipoServicioSeleccionadoEjecutar(tipoServicioSeleccionado);
                }
            }
        }
        
        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<ServicioMD> Servicios { get; set; }
        public ObservableCollection<CatalogoMD> TipoPago { get; set; }
        public ObservableCollection<CatalogoMD> TipoReferencia { get; set; }

        public DelegateCommand CosultarCmd { get; set; }

        public bool MostrarTipo { get; set; }
        public int Longitud { get; set; }
        public Keyboard Teclado { get; set; }
        public string Contrapartida { get; set; }
        public List<ServicioMS> _servicios { get; set; }

        private ServicioMD servicioSeleccionado;
        private CatalogoMD tipoServicioSeleccionado;

        public ListaServiciosPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            Teclado = Keyboard.Default;

            CosultarCmd = new DelegateCommand(ConsultarEjecutar);

            MostrarTipo = true;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            TipoPago = new ObservableCollection<CatalogoMD>();
            TipoReferencia = new ObservableCollection<CatalogoMD>();
            Servicios = new ObservableCollection<ServicioMD>();
        }

        private async void ConsultarEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    ConsultaME consulta = new ConsultaME()
                    {
                        Descripcion = "Consulta Movil",
                        FechaConsulta = DateTime.Now,
                        Referencia = Contrapartida,
                        ServicioId = Servicio.ServicioId,
                        UsuarioId = DatosGlobales.Obtiene().UsuarioId,
                    };

                    var mensajeSalida = await _servicioUsuario.RealizaConsulta(consulta);

                    if (mensajeSalida.Rubros != null)
                    {
                        double valorAPagar = 0;

                        foreach (var item in mensajeSalida.Rubros)
                            valorAPagar += item.ValorAPagar;

                        NavigationParameters parameters = new NavigationParameters()
                        {
                            {"Consulta", mensajeSalida},
                            {"Contrapartida", Contrapartida},
                            {"TipoServicio", Servicio.TipoServicio.Nombre},
                            {"Servicio", Servicio},
                            {"Rubros", mensajeSalida.Rubros},
                            {"ValorApagar", valorAPagar}
                        };

                        if (Servicio.TipoPago.Id == 1)
                            await _navigationService.NavigateAsync("ElegirPagoPage", parameters);
                        else
                            await _navigationService.NavigateAsync("InformacionPagoPage", parameters);
                    }
                    else
                    {
                        await _userDialogs.AlertAsync("No se encuentran valores a pagar");
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void ServicioSeleccionadoEjecutar(ServicioMD servicioSeleccionado)
        {
            try
            {
                if(servicioSeleccionado != null)
                {
                    Longitud = servicioSeleccionado.LongitudReferencia;
                    if (servicioSeleccionado.TipoReferencia.Nombre.ToUpper().Contains("NUM"))
                    {
                        Teclado = Keyboard.Numeric;
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
            
        }

        private void TipoServicioSeleccionadoEjecutar(CatalogoMD tipoServicioSeleccionado)
        {
            try
            {
                if (tipoServicioSeleccionado != null)
                {
                    if (tipoServicioSeleccionado.Id != 0)
                    {
                        Servicios.Clear();

                        foreach (var item in _servicios)
                        {
                            if (tipoServicioSeleccionado.Id == item.TipoServicioId)
                            {
                                ServicioMD dato = new ServicioMD()
                                {
                                    ServicioId = item.ServicioId,
                                    Activo = item.EstaActivo,
                                    ComisionRubro = item.ComisionRubro,
                                    LongitudReferencia = item.LongitudReferencia,
                                    Nombre = item.Nombre,
                                    ServicioConsulta = item.ServicioConsulta,
                                    ServicioPago = item.ServicioPago,
                                    TipoServicio = TiposServicios.Where(u => u.Id == item.TipoServicioId).FirstOrDefault(),
                                    TipoPago = TipoPago.Where(u => u.Id == item.TipoPagoId).FirstOrDefault(),
                                    TipoReferencia = TipoReferencia.Where(u => u.Id == item.TipoReferenciaId).FirstOrDefault(),
                                };
                                Servicios.Add(dato);
                            }
                        }
                    }
                }
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
                var tipoPago = await _servicioUsuario.ListaTipoPago();
                var tiporeferencia = await _servicioUsuario.ListaTipoReferencia();
                var tipoServicio = await _servicioUsuario.ListaTipoServicios();
                Servicios.Clear();
                TipoPago.Clear();
                TipoReferencia.Clear();

                foreach (var item in tiporeferencia)
                {
                    CatalogoMD dato = new CatalogoMD()
                    {
                        Id = item.TipoReferenciaId,
                        EstaActivo = item.EstaActivo,
                        Nombre = item.Nombre
                    };
                    TipoReferencia.Add(dato);
                }

                foreach (var item in tipoPago)
                {
                    CatalogoMD dato = new CatalogoMD()
                    {
                        Id = item.TipoPagoId,
                        EstaActivo = item.EstaActivo,
                        Nombre = item.Nombre
                    };
                    TipoPago.Add(dato);
                }

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

                foreach (var item in _servicios)
                {
                    ServicioMD dato = new ServicioMD()
                    {
                        ServicioId = item.ServicioId,
                        Activo = item.EstaActivo,
                        ComisionRubro = item.ComisionRubro,
                        LongitudReferencia = item.LongitudReferencia,
                        Nombre = item.Nombre,
                        ServicioConsulta = item.ServicioConsulta,
                        ServicioPago = item.ServicioPago,
                        TipoServicio = TiposServicios.Where(u => u.Id == item.TipoServicioId).FirstOrDefault(),
                        TipoPago = TipoPago.Where(u => u.Id == item.TipoPagoId).FirstOrDefault(),
                        TipoReferencia = TipoReferencia.Where(u => u.Id == item.TipoReferenciaId).FirstOrDefault(),
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
            if (parameters.ContainsKey("Servicios"))
            {
                _servicios = parameters["Servicios"] as List<ServicioMS>;

                ObtenerDatos();
            }

            if (parameters.ContainsKey("TipoServicio"))
            {
                MostrarTipo = false;
                TipoServicio = parameters["TipoServicio"] as CatalogoMD;
            }
        }
    }
}
using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
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
    public class CatalogosPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand<CatalogoMD> TipoPagoSeleccionarCmd { get; set; }
        public DelegateCommand<CatalogoMD> TipoReferenciaSeleccionarCmd { get; set; }
        public DelegateCommand<CatalogoMD> TipoServicioSeleccionarCmd { get; set; }
        public DelegateCommand AgregarTipoServicioCmd { get; set; }
        public DelegateCommand GuardarTipoServicioCmd { get; set; }
        public DelegateCommand EliminarTipoServicioCmd { get; set; }
        public DelegateCommand AgregarTipoReferenciaCmd { get; set; }
        public DelegateCommand GuardarTipoReferenciaCmd { get; set; }
        public DelegateCommand EliminarTipoReferenciaCmd { get; set; }
        public DelegateCommand AgregarTipoPagoCmd { get; set; }
        public DelegateCommand GuardarTipoPagoCmd { get; set; }
        public DelegateCommand EliminarTipoPagoCmd { get; set; }

        public ObservableCollection<CatalogoMD> TiposServicios { get; set; }
        public ObservableCollection<CatalogoMD> TiposReferencias { get; set; }
        public ObservableCollection<CatalogoMD> TiposPagos { get; set; }

        public CatalogoMD TipoReferencia { get; set; }
        public CatalogoMD TipoServicio { get; set; }
        public CatalogoMD TipoPago { get; set; }

        public string IconoBotonTipoReferencia { get; set; }
        public string IconoBotonTipoServicio { get; set; }
        public string IconoBotonTipoPago { get; set; }
        public bool MostrarListaTipoReferencia { get; set; }
        public bool MostrarListaTipoServicio { get; set; }
        public bool MostrarListaTipoPago { get; set; }
        public bool MostrarEditarTipoReferencia { get; set; }
        public bool MostrarEditarTipoServicio { get; set; }
        public bool MostrarEditarTipoPago { get; set; }

        public CatalogosPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            TiposServicios = new ObservableCollection<CatalogoMD>();
            TiposReferencias = new ObservableCollection<CatalogoMD>();
            TiposPagos = new ObservableCollection<CatalogoMD>();

            TipoReferencia = new CatalogoMD();
            TipoServicio = new CatalogoMD();
            TipoPago = new CatalogoMD();

            TipoPagoSeleccionarCmd = new DelegateCommand<CatalogoMD>(TipoPagoSeleccionarEjecutar);
            AgregarTipoPagoCmd = new DelegateCommand(AgregarTipoPagoEjecutar);
            GuardarTipoPagoCmd = new DelegateCommand(GuardarTipoPagoEjecutar);
            EliminarTipoPagoCmd = new DelegateCommand(EliminarTipoPagoEjecutar); 
            TipoServicioSeleccionarCmd = new DelegateCommand<CatalogoMD>(TipoServicioSeleccionarEjecutar);
            AgregarTipoServicioCmd = new DelegateCommand(AgregarTipoServicioEjecutar);
            GuardarTipoServicioCmd = new DelegateCommand(GuardarTipoServicioEjecutar);
            EliminarTipoServicioCmd = new DelegateCommand(EliminarTipoServicioEjecutar);
            TipoReferenciaSeleccionarCmd = new DelegateCommand<CatalogoMD>(TipoReferenciaSeleccionarEjecutar);
            AgregarTipoReferenciaCmd = new DelegateCommand(AgregarTipoReferenciaEjecutar);
            GuardarTipoReferenciaCmd = new DelegateCommand(GuardarTipoReferenciaEjecutar);
            EliminarTipoReferenciaCmd = new DelegateCommand(EliminarTipoReferenciaEjecutar);
        }

        #region TipoPago
        private async void EliminarTipoPagoEjecutar()
        {
            try
            {
                var respuesta = await _userDialogs.ConfirmAsync("¿Está seguro de eliminar el tipo de pago?", "Eliminar", "Si", "No");

                if (respuesta)
                {
                    using (_userDialogs.Loading("Cargando"))
                    {
                        var mensajeSalida = await _servicioUsuario.EliminarTipoPago(TipoPago.Id);
                        Inicializar();
                        ObtenerDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void GuardarTipoPagoEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    CatalogoValidator catalogoValidador = new CatalogoValidator();
                    FluentValidation.Results.ValidationResult validationResult = catalogoValidador.Validate(TipoPago);

                    if (!validationResult.IsValid)
                        _userDialogs.Alert(ErroresValidacion.Despliega(validationResult));

                    else
                    {
                        TipoPagoME mensajeEntrada = new TipoPagoME()
                        {
                            EstaActivo = TipoPago.EstaActivo,
                            Nombre = TipoPago.Nombre,
                            TipoPagoId = TipoPago.Id
                        };

                        if (mensajeEntrada.TipoPagoId == 0)
                        {
                            var mensajeSalida = await _servicioUsuario.GuardarTipoPago(mensajeEntrada);

                            if (mensajeSalida)
                                await _userDialogs.AlertAsync("Se ha agregado un nuevo tipo de pago");
                            else
                                await _userDialogs.AlertAsync("No se ha podido agregar");
                        }
                        else
                        {
                            var mensajeSalida = await _servicioUsuario.ActualizarTipoPago(mensajeEntrada);

                            if (mensajeSalida)
                                await _userDialogs.AlertAsync("Se ha actualizado el tipo de pago");
                            else
                                await _userDialogs.AlertAsync("No se ha podido actualizar");
                        }
                        Inicializar();
                        ObtenerDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void AgregarTipoPagoEjecutar()
        {
            try
            {
                if (IconoBotonTipoPago == "\uf067")
                {
                    Inicializar();
                    MostrarEditarTipoPago = true;
                    MostrarListaTipoReferencia = true;
                    MostrarListaTipoServicio = true;
                    IconoBotonTipoPago = "\uf060";
                    TipoPago = new CatalogoMD();
                }
                else
                {
                    Inicializar();
                    ObtenerDatos();
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void TipoPagoSeleccionarEjecutar(CatalogoMD item)
        {
            try
            {
                Inicializar();
                MostrarEditarTipoPago = true;
                IconoBotonTipoPago = "\uf060";

                TipoPago = new CatalogoMD()
                {
                    EstaActivo = item.EstaActivo,
                    Id = item.Id,
                    Nombre = item.Nombre
                };
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }
        #endregion

        #region TipoReferencia
        private async void EliminarTipoReferenciaEjecutar()
        {
            try
            {
                var respuesta = await _userDialogs.ConfirmAsync("¿Está seguro de eliminar el tipo de referencia?", "Eliminar", "Si", "No");

                if (respuesta)
                {
                    using (_userDialogs.Loading("Cargando"))
                    {
                        var mensajeSalida = await _servicioUsuario.EliminarTipoReferencia(TipoReferencia.Id);
                        Inicializar();
                        ObtenerDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void GuardarTipoReferenciaEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    CatalogoValidator catalogoValidador = new CatalogoValidator();
                    FluentValidation.Results.ValidationResult validationResult = catalogoValidador.Validate(TipoReferencia);

                    if (!validationResult.IsValid)
                        _userDialogs.Alert(ErroresValidacion.Despliega(validationResult));

                    else
                    {
                        TipoReferenciaME mensajeEntrada = new TipoReferenciaME()
                        {
                            EstaActivo = TipoReferencia.EstaActivo,
                            Nombre = TipoReferencia.Nombre,
                            TipoReferenciaId = TipoReferencia.Id
                        };

                        if (mensajeEntrada.TipoReferenciaId == 0)
                        {
                            var mensajeSalida = await _servicioUsuario.GuardarTipoReferencia(mensajeEntrada);

                            if (mensajeSalida)
                                await _userDialogs.AlertAsync("Se ha agregado un nuevo tipo de referencia");
                            else
                                await _userDialogs.AlertAsync("No se ha podido agregar");
                        }
                        else
                        {
                            var mensajeSalida = await _servicioUsuario.ActualizarTipoReferencia(mensajeEntrada);

                            if (mensajeSalida)
                                await _userDialogs.AlertAsync("Se ha actualizado el tipo de referencia");
                            else
                                await _userDialogs.AlertAsync("No se ha podido actualizar");
                        }
                        Inicializar();
                        ObtenerDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void AgregarTipoReferenciaEjecutar()
        {
            try
            {
                if (IconoBotonTipoReferencia == "\uf067")
                {
                    Inicializar();
                    MostrarEditarTipoReferencia = true;
                    MostrarListaTipoServicio = true;
                    MostrarListaTipoPago = true;
                    IconoBotonTipoReferencia = "\uf060";
                    TipoReferencia = new CatalogoMD();
                }
                else
                {
                    Inicializar();
                    ObtenerDatos();
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void TipoReferenciaSeleccionarEjecutar(CatalogoMD item)
        {
            try
            {
                Inicializar();
                MostrarEditarTipoReferencia = true;
                IconoBotonTipoReferencia = "\uf060";

                TipoReferencia = new CatalogoMD()
                {
                    EstaActivo = item.EstaActivo,
                    Id = item.Id,
                    Nombre = item.Nombre
                };
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }
        #endregion

        #region TipoServicio
        private async void EliminarTipoServicioEjecutar()
        {
            try
            {
                var respuesta = await _userDialogs.ConfirmAsync("¿Está seguro de eliminar el tipo de servicio?", "Eliminar", "Si", "No");

                if (respuesta)
                {
                    using (_userDialogs.Loading("Cargando"))
                    {
                        var mensajeSalida = await _servicioUsuario.EliminarTipoServicio(TipoServicio.Id);
                        Inicializar();
                        ObtenerDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async void GuardarTipoServicioEjecutar()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    CatalogoValidator catalogoValidador = new CatalogoValidator();
                    FluentValidation.Results.ValidationResult validationResult = catalogoValidador.Validate(TipoServicio);

                    if (!validationResult.IsValid)
                        _userDialogs.Alert(ErroresValidacion.Despliega(validationResult));

                    else
                    {
                        TipoServicioME mensajeEntrada = new TipoServicioME()
                        {
                            EstaActivo = TipoServicio.EstaActivo,
                            Nombre = TipoServicio.Nombre,
                            TipoServicioId = TipoServicio.Id
                        };

                        if (mensajeEntrada.TipoServicioId == 0)
                        {
                            var mensajeSalida = await _servicioUsuario.GuardarTipoServicio(mensajeEntrada);

                            if (mensajeSalida)
                                await _userDialogs.AlertAsync("Se ha agregado un nuevo tipo de servicio");
                            else
                                await _userDialogs.AlertAsync("No se ha podido agregar");
                        }
                        else
                        {
                            var mensajeSalida = await _servicioUsuario.ActualizarTipoServicio(mensajeEntrada);

                            if (mensajeSalida)
                                await _userDialogs.AlertAsync("Se ha actualizado el tipo de servicio");
                            else
                                await _userDialogs.AlertAsync("No se ha podido actualizar");
                        }
                        Inicializar();
                        ObtenerDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void AgregarTipoServicioEjecutar()
        {
            try
            {
                if (IconoBotonTipoServicio == "\uf067")
                {
                    Inicializar();
                    MostrarEditarTipoServicio = true;
                    MostrarListaTipoReferencia = true;
                    MostrarListaTipoPago = true;
                    IconoBotonTipoServicio = "\uf060";
                    TipoServicio = new CatalogoMD();
                }
                else
                {
                    Inicializar();
                    ObtenerDatos();
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void TipoServicioSeleccionarEjecutar(CatalogoMD item)
        {
            try
            {
                Inicializar();
                MostrarEditarTipoServicio = true;
                IconoBotonTipoServicio = "\uf060";

                TipoServicio = new CatalogoMD()
                {
                    EstaActivo = item.EstaActivo,
                    Id = item.Id,
                    Nombre = item.Nombre
                };
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }
        #endregion

        private async void ObtenerDatos()
        {
            try
            {
                using (_userDialogs.Loading("Cargando"))
                {
                    MostrarListaTipoReferencia = true;
                    MostrarListaTipoServicio = true;
                    MostrarListaTipoPago = true;

                    var mensajeSalidaTipoPago = await _servicioUsuario.ListaTipoPago();
                    var mensajeSalidaTipoReferencia = await _servicioUsuario.ListaTipoReferencia();
                    var mensajeSalidaTipoServicio = await _servicioUsuario.ListaTipoServicios();

                    TiposPagos.Clear();
                    TiposServicios.Clear();
                    TiposReferencias.Clear();

                    foreach (var item in mensajeSalidaTipoPago)
                    {
                        if (item.EstaActivo)
                        {
                            CatalogoMD catalogo = new CatalogoMD()
                            {
                                Nombre = item.Nombre,
                                Id = item.TipoPagoId,
                                EstaActivo = item.EstaActivo
                            };
                            TiposPagos.Add(catalogo);
                        }
                    }

                    foreach (var item in mensajeSalidaTipoServicio)
                    {
                        if (item.EstaActivo)
                        {
                            CatalogoMD catalogo = new CatalogoMD()
                            {
                                Nombre = item.Nombre,
                                Id = item.TipoServicioId,
                                EstaActivo = item.EstaActivo
                            };
                            TiposServicios.Add(catalogo);
                        }
                    }

                    foreach (var item in mensajeSalidaTipoReferencia)
                    {
                        if (item.EstaActivo)
                        {
                            CatalogoMD catalogo = new CatalogoMD()
                            {
                                Nombre = item.Nombre,
                                Id = item.TipoReferenciaId,
                                EstaActivo = item.EstaActivo
                            };
                            TiposReferencias.Add(catalogo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private void Inicializar()
        {
            IconoBotonTipoReferencia = "\uf067";
            IconoBotonTipoServicio = "\uf067";
            IconoBotonTipoPago = "\uf067";
            MostrarListaTipoReferencia = false;
            MostrarListaTipoServicio = false;
            MostrarListaTipoPago = false;
            MostrarEditarTipoReferencia = false;
            MostrarEditarTipoServicio = false;
            MostrarEditarTipoPago = false;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Inicializar();
            ObtenerDatos();
        }

    }
}

using AppTesisPagoServicios.Models.MensajeriaEntrada;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTesisPagoServicios
{
    public interface IServicioUsuario
    {
        #region Tipo Servicio
        Task<List<TipoServicioMS>> ListaTipoServicios();
        Task<bool> GuardarTipoServicio(TipoServicioME mensajeEntrada);
        Task<bool> ActualizarTipoServicio(TipoServicioME mensajeEntrada);
        Task<bool> EliminarTipoServicio(int mensajeEntrada);
        #endregion

        #region Usuario
        Task<LoginMS> Login(LoginME mensajeEntrada);
        Task<bool> RegistrarUsuario(UsuarioME mensajeEntrada);
        #endregion

        #region Tipo Referencia
        Task<List<TipoReferenciaMS>> ListaTipoReferencia();
        Task<bool> GuardarTipoReferencia(TipoReferenciaME mensajeEntrada);
        Task<bool> ActualizarTipoReferencia(TipoReferenciaME mensajeEntrada);
        Task<bool> EliminarTipoReferencia(int mensajeEntrada);
        #endregion

        #region Tipo Pago
        Task<List<TipoPagoMS>> ListaTipoPago();
        Task<bool> GuardarTipoPago(TipoPagoME mensajeEntrada);
        Task<bool> ActualizarTipoPago(TipoPagoME mensajeEntrada);
        Task<bool> EliminarTipoPago(int mensajeEntrada);
        #endregion

        #region Servicio
        Task<List<ServicioMS>> ListaServicios();
        Task<bool> GuardarServicio(ServicioME mensajeEntrada);
        Task<bool> ActualizarServicio(ServicioME mensajeEntrada);
        Task<bool> EliminarServicio(int mensajeEntrada);
        #endregion

        #region Parametrizacion
        Task<List<ParametrizacionMS>> ListaParametrizacion();
        Task<bool> GuardarParametrizacion(ParametrizacionME mensajeEntrada);
        Task<bool> ActualizarParametrizacion(ParametrizacionME mensajeEntrada);
        #endregion

        #region Consulta
        Task<List<ConsultaMS>> ListaConsultas();
        Task<List<ConsultaMS>> ListaConsultasFechas(ConsultaFechaME mensajeEntrada);
        Task<ConsultaMS> RealizaConsulta(ConsultaME mensajeEntrada);
        #endregion

        #region ServicioUsuario
        Task<List<ServicioUsuarioMS>> ListaServiciosUsuarios(int mensajeEntrada);
        Task<bool> GuardarServicioUsuario(ServicioUsuarioME mensajeEntrada);
        Task<bool> ActualizarServicioUsuario(ServicioUsuarioME mensajeEntrada);
        Task<bool> EliminarServicioUsuario(int mensajeEntrada);
        #endregion

        #region OTP
        Task<bool> EnviarOTP(string mensajeEntrada);
        Task<bool> ValidaOTP(OtpME mensajeEntrada);
        #endregion

        #region Pago
        Task<PagoMS> RealizarPago(PagoME mensajeEntrada);
        Task<List<PagoMS>> ListaPagoFechas(ConsultaFechaME mensajeEntrada);
        Task<List<PagoMS>> ListaPagos();
        #endregion
    }
}

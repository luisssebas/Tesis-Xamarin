using AppTesisPagoServicios.Models.MensajeriaEntrada;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using AppTesisPagoServicios.Recursos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTesisPagoServicios.Services
{
    public class ServicioUsuario : IServicioUsuario
    {
        public static string ApiUrl = "http://192.168.100.39:6969/api";

        #region Tipo Servicio
        public async Task<List<TipoServicioMS>> ListaTipoServicios()
        {
            List<TipoServicioMS> mensajeSalida = new List<TipoServicioMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.TipoServicio_ListarTipoServicios;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<TipoServicioMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> GuardarTipoServicio(TipoServicioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.TipoServicio_GuardarTipoServicio;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ActualizarTipoServicio(TipoServicioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.TipoServicio_ActualizarTipoServicio;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> EliminarTipoServicio(int mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.TipoServicio_EliminarTipoServicio + "/" + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Usuario
        public async Task<LoginMS> Login(LoginME mensajeEntrada)
        {
            LoginMS mensajeSalida = new LoginMS();
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Usuario_LoginUsuario;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<LoginMS>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> RegistrarUsuario(UsuarioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Usuario_GuardarUsuario;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Tipo Referencia
        public async Task<List<TipoReferenciaMS>> ListaTipoReferencia()
        {
            List<TipoReferenciaMS> mensajeSalida = new List<TipoReferenciaMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.TipoReferencia_ListarTipoReferencia;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<TipoReferenciaMS>>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> GuardarTipoReferencia(TipoReferenciaME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.TipoReferencia_GuardarTipoReferencia;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ActualizarTipoReferencia(TipoReferenciaME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.TipoReferencia_ActualizarTipoReferencia;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> EliminarTipoReferencia(int mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.TipoReferencia_EliminarTipoReferencia + "/" + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Tipo Pago
        public async Task<List<TipoPagoMS>> ListaTipoPago()
        {
            List<TipoPagoMS> mensajeSalida = new List<TipoPagoMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.TipoPago_ListarTipoPagos;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<TipoPagoMS>>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> GuardarTipoPago(TipoPagoME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.TipoPago_GuardarTipoPago;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ActualizarTipoPago(TipoPagoME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.TipoPago_ActualizarTipoPago;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> EliminarTipoPago(int mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.TipoPago_EliminarTipoPago + "/" + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Servicio
        public async Task<List<ServicioMS>> ListaServicios()
        {
            List<ServicioMS> mensajeSalida = new List<ServicioMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.Servicio_ListarServicios;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<ServicioMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> GuardarServicio(ServicioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Servicio_GuardarServicio;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ActualizarServicio(ServicioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Servicio_ActualizarServicio;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> EliminarServicio(int mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.Servicio_EliminarServicio + "/" + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Parametrizacion
        public async Task<List<ParametrizacionMS>> ListaParametrizacion()
        {
            List<ParametrizacionMS> mensajeSalida = new List<ParametrizacionMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.Parametrizacion_ListarUsuarios;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<ParametrizacionMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> GuardarParametrizacion(ParametrizacionME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Parametrizacion_GuardarUsuario;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ActualizarParametrizacion(ParametrizacionME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Parametrizacion_ActualizarUsuario;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Consulta
        public async Task<List<ConsultaMS>> ListaConsultas()
        {
            List<ConsultaMS> mensajeSalida = new List<ConsultaMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.Colsulta_ListaConsultas;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<ConsultaMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<List<ConsultaMS>> ListaConsultasFechas(ConsultaFechaME mensajeEntrada)
        {
            List<ConsultaMS> mensajeSalida = new List<ConsultaMS>();
            try
            {
                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Consulta_ListaConsultasPorFecha;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<ConsultaMS>> (responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<ConsultaMS> RealizaConsulta(ConsultaME mensajeEntrada)
        {
            ConsultaMS mensajeSalida = new ConsultaMS();
            try
            {
                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Consulta_RealizaConsulta;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<ConsultaMS>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region ServicioUsuario
        public async Task<List<ServicioUsuarioMS>> ListaServiciosUsuarios(int mensajeEntrada)
        {
            List<ServicioUsuarioMS> mensajeSalida = new List<ServicioUsuarioMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.ServicioUsuario_ListarServiciosUsuarios + "/" + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<ServicioUsuarioMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> GuardarServicioUsuario(ServicioUsuarioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.ServicioUsuario_GuardarServicioUsuario;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ActualizarServicioUsuario(ServicioUsuarioME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.ServicioUsuario_ActualizarServicioUsuario;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> EliminarServicioUsuario(int mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.ServicioUsuario_EliminarServicioUsuario + "/" + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region OTP
        public async Task<bool> EnviarOTP(string mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.Usuario_EnviarOTP + mensajeEntrada;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<bool> ValidaOTP(OtpME mensajeEntrada)
        {
            bool mensajeSalida = false;
            try
            {
                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Usuario_ValidarOTP;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

        #region Pago
        public async Task<PagoMS> RealizarPago(PagoME mensajeEntrada)
        {
            PagoMS mensajeSalida = new PagoMS();
            try
            {
                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Pago_RealizarPago;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<PagoMS>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<List<PagoMS>> ListaPagoFechas(ConsultaFechaME mensajeEntrada)
        {
            List <PagoMS> mensajeSalida = new List<PagoMS>();
            try
            {
                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + ApiResource.Pago_ListaPagoFechas;
                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<PagoMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }

        public async Task<List<PagoMS>> ListaPagos()
        {
            List<PagoMS> mensajeSalida = new List<PagoMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + ApiResource.Pago_ListaPago;
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                mensajeSalida = JsonConvert.DeserializeObject<List<PagoMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensajeSalida;
        }
        #endregion

    }
}
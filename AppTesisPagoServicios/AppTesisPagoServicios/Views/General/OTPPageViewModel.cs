using Acr.UserDialogs;
using AppTesisPagoServicios.Helpers;
using AppTesisPagoServicios.Models.DTO;
using AppTesisPagoServicios.Models.MensajeriaEntrada;
using AppTesisPagoServicios.Models.MensajeriaSalida;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class OTPPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand<string> ValidarOTPCmd { get; set; }

        public InformacionPagoMD InformacionPago { get; set; }
        public ServicioMD Servicio { get; set; }

        public string ValorOTP { get; set; }

        private ConsultaMS _consulta;
        private List<RubrosMS> _rubros;
        private string _TipoServicio;
        private string _contrapartida;

        public OTPPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            ValidarOTPCmd = new DelegateCommand<string>(ValidarOTPEjecutar);
            ValorOTP = "";
        }

        private async void ValidarOTPEjecutar(string otp)
        {
            try
            {
                bool respuesta = false;
                using (_userDialogs.Loading("Cargando"))
                {
                    OtpME mensajeEntrada = new OtpME()
                    {
                        OTP = otp,
                        Username = DatosGlobales.Obtiene().Username
                    };

                    respuesta = await _servicioUsuario.ValidaOTP(mensajeEntrada);
                }

                if (respuesta)
                {
                    var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Pago One Pay", (decimal)InformacionPago.ValorTotalDato, "USD"),
                    new Decimal(0.12));
                    if (result.Status == PayPalStatus.Cancelled)
                    {
                        await _userDialogs.AlertAsync("Cancelado");
                    }
                    else if (result.Status == PayPalStatus.Error)
                    {
                        await _userDialogs.AlertAsync(result.ErrorMessage);
                    }
                    else if (result.Status == PayPalStatus.Successful)
                    {
                        using (_userDialogs.Loading("Cargando"))
                        {
                            PagoME mensajePagoME = new PagoME()
                            {
                                ConsultaId = _consulta.ConsultaId,
                                ServicioId = Servicio.ServicioId,
                                UsuarioId = DatosGlobales.Obtiene().UsuarioId,
                                ValorPagado = InformacionPago.ValorTotalDato,
                                Rubros = _rubros,
                                IdPasarelaPago = result.ServerResponse.Response.Id
                            };

                            var pagoMS = await _servicioUsuario.RealizarPago(mensajePagoME);

                            NavigationParameters parameters = new NavigationParameters()
                            {
                                {"Consulta", _consulta},
                                {"Contrapartida", _consulta},
                                {"Servicio", _consulta},
                                {"TipoServicio", _consulta},
                                {"Rubros", _consulta},
                                {"InformacionPago", InformacionPago},
                                {"Pago", pagoMS},
                                {"PaypalID", result.ServerResponse.Response.Id},
                            };

                            await _navigationService.NavigateAsync("PagoCompletoPage", parameters);
                        }
                    }
                }
                else
                {
                    await _userDialogs.AlertAsync("El código OTP ingresado está incorrecto");
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
            if (parameters.ContainsKey("Consulta"))
                _consulta = parameters["Consulta"] as ConsultaMS;

            if (parameters.ContainsKey("Contrapartida"))
                _contrapartida = parameters["Contrapartida"].ToString();

            if (parameters.ContainsKey("Servicio"))
                Servicio = parameters["Servicio"] as ServicioMD;

            if (parameters.ContainsKey("TipoServicio"))
                _TipoServicio = parameters["TipoServicio"].ToString();

            if (parameters.ContainsKey("Rubros"))
                _rubros = parameters["Rubros"] as List<RubrosMS>;

            if(parameters.ContainsKey("InformacionPago"))
                InformacionPago = parameters["InformacionPago"] as InformacionPagoMD;
        }
    }
}

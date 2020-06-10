using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaEntrada
{
    public class ParametrizacionME
    {
        public int ParametrizacionId { get; set; }
        public double Comision { get; set; }
        public int MaxIntentosLogin { get; set; }
        public string Correo { get; set; }
        public string ContraseniaCorreo { get; set; }
        public string SmtpCorreo { get; set; }
        public string PuertoCorreo { get; set; }
        public string MensajeRecuperacionContrasenia { get; set; }
        public string MensajePagoServicio { get; set; }
        public string MensajeNuevaCuenta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class ParametrizacionMD
    {
        public int ParametrizacionId { get; set; }
        public double Comision { get; set; }
        public int MaxIntentosLogin { get; set; }
        public string Correo { get; set; }
        public string ContraseniaCorreo { get; set; }
        public string MensajeRecuperacionContrasenia { get; set; }
        public string MensajePagoServicio { get; set; }
        public string MensajeNuevaCuenta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaSalida
{
    public class ServicioMS
    {
        public int ServicioId { get; set; }
        public int TipoPagoId { get; set; }
        public int TipoServicioId { get; set; }
        public int TipoReferenciaId { get; set; }
        public string Nombre { get; set; }
        public int LongitudReferencia { get; set; }
        public bool ComisionRubro { get; set; }
        public string ServicioConsulta { get; set; }
        public string ServicioPago { get; set; }
        public bool EstaActivo { get; set; }
    }
}

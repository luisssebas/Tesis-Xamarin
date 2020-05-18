using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaEntrada
{
    public class ServicioME
    {
        public int ServicioId { get; set; }
        public int TipoPagoId { get; set; }
        public int TipoServicioId { get; set; }
        public int TipoReferenciaId { get; set; }
        public string Nombre { get; set; }
        public int LongitudReferencia { get; set; }
        public bool ComisionRubro { get; set; }
        public string ServicioHttp { get; set; }
        public bool EstaActivo { get; set; }
    }
}

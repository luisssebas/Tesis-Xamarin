using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaEntrada
{
    public class TipoReferenciaME
    {
        public int TipoReferenciaId { get; set; }
        public string Nombre { get; set; }
        public bool EstaActivo { get; set; }
    }
}

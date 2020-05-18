using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaSalida
{
    public class TipoReferenciaMS
    {
        public int TipoReferenciaId { get; set; }
        public string Nombre { get; set; }
        public bool EstaActivo { get; set; }
    }
}

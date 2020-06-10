using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaSalida
{
    public class RubrosMS
    {
        public int Prioridad { get; set; }
        public double ValorAPagar { get; set; }
        public string Periodo { get; set; }
        public string Descripcion { get; set; }
    }
}

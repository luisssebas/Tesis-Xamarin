using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class RubrosMD
    {
        public int Prioridad { get; set; }
        public double ValorAPagar { get; set; }
        public string Periodo { get; set; }
        public bool Seleccionado { get; set; }
        public string Descripcion { get; set; }
    }
}

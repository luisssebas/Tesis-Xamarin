using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTesisPagoServicios.Models.DTO
{
    public class InformacionPagoMD
    {
        public double ValorAPagar { get; set; }
        public double Comision { get; set; }
        public string TipoServicio { get; set; }
        public string Servicio { get; set; }
        public string Contrapartida { get; set; }
        public FormattedString ValorTotal { get; set; }
    }
}

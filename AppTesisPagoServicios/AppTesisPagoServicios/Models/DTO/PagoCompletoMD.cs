using AppTesisPagoServicios.Models.MensajeriaSalida;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTesisPagoServicios.Models.DTO
{
    public class PagoCompletoMD
    {
        public string Documento { get; set; }
        public FormattedString ValorPagado { get; set; }
        public double Comision { get; set; }
        public string TipoServicio { get; set; }
        public string Contrapartida { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string PaypalID { get; set; }
        public string Fecha { get; set; }
        public List<RubrosMS> Rubros { get; set; }

        public PagoCompletoMD()
        {
            Rubros = new List<RubrosMS>();
        }

    }
}

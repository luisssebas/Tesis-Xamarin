using AppTesisPagoServicios.Models.MensajeriaSalida;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaEntrada
{
    public class PagoME
    {
        public int PagoId { get; set; }
        public int ServicioId { get; set; }
        public int ConsultaId { get; set; }
        public int UsuarioId { get; set; }
        public double ValorPagado { get; set; }
        public string IdPasarelaPago { get; set; }
        public List<RubrosMS> Rubros { get; set; }
    }
}

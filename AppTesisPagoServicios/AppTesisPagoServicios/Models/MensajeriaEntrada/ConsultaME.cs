using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaEntrada
{
    public class ConsultaME
    {
        public int ConsultaId { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        public int UsuarioId { get; set; }
        public int ServicioId { get; set; }
    }
}

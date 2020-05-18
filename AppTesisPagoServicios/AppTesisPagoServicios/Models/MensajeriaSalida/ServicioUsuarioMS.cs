using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaSalida
{
    public class ServicioUsuarioMS
    {
        public int ServicioId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Contrapartida { get; set; }
    }
}

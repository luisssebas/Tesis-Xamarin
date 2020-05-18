using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.MensajeriaSalida
{
    public class LoginMS
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool EstaActivo { get; set; }
        public bool EsAdmin { get; set; }
        public string Mensaje { get; set; }
    }
}

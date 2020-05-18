using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class MenuMD
    {
        public string Vista { get; set; }
        public string MenuTitle { get; set; }
        public string MenuDetail { get; set; }
        public string Imagen { get; set; }
        public string ColorFondo { get; set; }
        public string Color { get; set; }
        public bool EsAdmin { get; set; }
        public bool EsUsuario { get; set; }
    }
}

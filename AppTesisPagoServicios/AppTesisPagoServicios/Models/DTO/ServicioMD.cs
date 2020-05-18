using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class ServicioMD
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public int LongitudReferencia { get; set; }
        public bool ComisionRubro { get; set; }
        public string ServicioHttp { get; set; }
        public string EstaActivo { get; set; }
        public bool Activo { get; set; }
        public CatalogoMD TipoPago { get; set; }
        public CatalogoMD TipoServicio { get; set; }
        public CatalogoMD TipoReferencia { get; set; }

        public ServicioMD()
        {
            TipoReferencia = new CatalogoMD();
            TipoServicio = new CatalogoMD();
            TipoPago = new CatalogoMD();
        }
    }
}

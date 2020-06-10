using FluentValidation;
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
        public string ServicioConsulta { get; set; }
        public string ServicioPago { get; set; }
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
    public class ServicioValidator : AbstractValidator<ServicioMD>
    {
        public ServicioValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("*Ingrese el nombre");
            RuleFor(x => x.LongitudReferencia).NotNull().NotEmpty().WithMessage("*Ingrese la longitud de la contrapartida");
            RuleFor(x => x.ComisionRubro).NotNull().NotEmpty().WithMessage("*Ingrese si tiene una comisión por rubro");
            RuleFor(x => x.ServicioConsulta).NotNull().NotEmpty().WithMessage("*Ingrese el servicio de consulta");
            RuleFor(x => x.ServicioPago).NotNull().NotEmpty().WithMessage("*Ingrese el servicio de pago");
            RuleFor(x => x.Activo).NotNull().NotEmpty().WithMessage("*Ingrese si está activo");
            RuleFor(x => x.TipoPago).NotNull().NotEmpty().WithMessage("*Seleccione un tipo de pago");
            RuleFor(x => x.TipoServicio).NotNull().NotEmpty().WithMessage("*Seleccione un tipo de servicio");
            RuleFor(x => x.TipoReferencia).NotNull().NotEmpty().WithMessage("*Seleccione un tipo de referencia");
        }
    }
}

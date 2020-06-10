using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class ServicioUsuarioMD
    {
        public int ServicioUsuarioId { get; set; }
        public int ServicioId { get; set; }
        public int UsuarioId { get; set; }
        public CatalogoMD TipoServicio { get; set; }
        public ServicioMD Servicio { get; set; }
        public string Contrapartida { get; set; }

        public ServicioUsuarioMD()
        {
            Servicio = new ServicioMD();
            TipoServicio = new CatalogoMD();
        }
    }

    public class ServicioUsuarioValidator : AbstractValidator<ServicioUsuarioMD>
    {
        public ServicioUsuarioValidator()
        {
            RuleFor(x => x.Contrapartida).NotNull().NotEmpty().WithMessage("*Ingrese contrapartida");
            RuleFor(x => x.Servicio).NotNull().NotEmpty().WithMessage("*Seleccione un servicio");
            RuleFor(x => x.TipoServicio).NotNull().NotEmpty().WithMessage("*Seleccione un tipo de servicio");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class CatalogoMD
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EstaActivo { get; set; }
    }

    public class CatalogoValidator : AbstractValidator<CatalogoMD>
    {
        public CatalogoValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("*Ingrese el nombre");
            RuleFor(x => x.EstaActivo).NotNull().NotEmpty().WithMessage("*Seleccione si está activo");
        }
    }
}

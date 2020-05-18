using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Models.DTO
{
    public class UsuarioMD
    {
        public string Usuario { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string ComprobacionContrasenia { get; set; }
    }
    
    public class UsuarioValidator : AbstractValidator<UsuarioMD>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Identificacion).NotNull().NotEmpty().WithMessage("*Ingrese la identificación");
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("*Ingrese el nombre");
            RuleFor(x => x.Correo).NotNull().NotEmpty().WithMessage("*Ingrese el correo");
            RuleFor(x => x.Usuario).NotNull().NotEmpty().WithMessage("*Ingrese el usuario");
            RuleFor(x => x.Contrasenia).NotNull().NotEmpty().WithMessage("*Ingrese la contraseña");
            RuleFor(x => x.ComprobacionContrasenia).NotNull().NotEmpty().WithMessage("*Ingrese nuevamente la contraseña");
        }
    }
}

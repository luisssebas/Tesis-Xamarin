using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Helpers
{
    public class ErroresValidacion
    {
        public static string Despliega(FluentValidation.Results.ValidationResult validationResult)
        {
            string mensajeError = "";

            foreach (FluentValidation.Results.ValidationFailure errores in validationResult.Errors)
                if (errores.ErrorMessage.Contains("*"))
                    mensajeError += errores.ErrorMessage + "\n";

            return mensajeError;
        }

    }
}

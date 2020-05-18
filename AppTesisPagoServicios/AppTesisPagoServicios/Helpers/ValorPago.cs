using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppTesisPagoServicios.Helpers
{
    public static class ValorPago
    {
        public static FormattedString DevuelveFormateado(double valor, int fontSize, Color color, FontAttributes fontAttribute)
        {
            var number = valor.ToString("N", new CultureInfo("en-US"));

            string value = number.Substring(0, number.IndexOf("."));
            var decimalValue = number.Substring(number.IndexOf("."));
            var fs = new FormattedString();

            fs.Spans.Add(new Span { Text = "$" + value, FontSize = fontSize, TextColor = color, FontAttributes = fontAttribute });
            fs.Spans.Add(new Span { Text = decimalValue.Length > 2 ? decimalValue.Substring(0, 3) : decimalValue, FontSize = fontSize / 2 + 4, TextColor = color, FontAttributes = fontAttribute });
            return fs;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Helpers
{
    public class DatosGlobales
    {
        public string Username { get; set; }
        public int UsuarioId { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool EsAdmin { get; set; }

        public DatosGlobales() { }

        public DatosGlobales(string username, int usuarioId, string identificacion, string nombre, string correo, bool esAdmin)
        {
            Username = username;
            UsuarioId = usuarioId;
            Identificacion = identificacion;
            Nombre = nombre;
            Correo = correo;
            EsAdmin = esAdmin;
        }

        public void Adiciona()
        {
            var recipeJson = JsonConvert.SerializeObject(this);

            if (App.Current.Properties.ContainsKey("DatosGlobales"))
                App.Current.Properties["DatosGlobales"] = recipeJson;
            else
                App.Current.Properties.Add("DatosGlobales", recipeJson);
            App.Current.SavePropertiesAsync();
        }

        public static DatosGlobales Obtiene()
        {
            DatosGlobales dato = new DatosGlobales();

            if (App.Current.Properties.ContainsKey("DatosGlobales"))
            {
                string datoGlobal = App.Current.Properties["DatosGlobales"] as string;
                dato = JsonConvert.DeserializeObject<DatosGlobales>(datoGlobal);
            }

            return dato;
        }
    }
}

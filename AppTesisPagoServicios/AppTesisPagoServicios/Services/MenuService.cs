using AppTesisPagoServicios.Models.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTesisPagoServicios.Services
{
    public class MenuServicio
    {
        public static ObservableCollection<MenuMD> DevuelveMenuAdministrador()
        {
            ObservableCollection<MenuMD> lista = new ObservableCollection<MenuMD>();
            MenuMD menu = new MenuMD();
            menu.MenuTitle = "Catálogos";
            menu.MenuDetail = "Catalogos";
            menu.Vista = "CatalogosTabbedPage";
            menu.Imagen = "\uf03a";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Servicio";
            menu.MenuDetail = "Servicio";
            menu.Vista = "ServicioAdminPage";
            menu.Imagen = "\uf0eb";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Parametrizaciones";
            menu.MenuDetail = "Parametrizaciones";
            menu.Vista = "ParametrizacionPage";
            menu.Imagen = "\uf249";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Reportes";
            menu.MenuDetail = "Reportes";
            menu.Vista = "ReporteBusquedaPage";
            menu.Imagen = "\uf0ce";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.ColorFondo = "#FFFFFF";
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Cerrar Sesión";
            menu.MenuDetail = "Cerrar Sesión";
            menu.Vista = "PantallaInicialPage";
            menu.Imagen = "\uf023";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = true;
            lista.Add(menu);

            return lista;
        }

        public static ObservableCollection<MenuMD> DevuelveMenuUsuario()
        {
            ObservableCollection<MenuMD> lista = new ObservableCollection<MenuMD>();
            MenuMD menu = new MenuMD();
            menu.MenuTitle = "Tipo de servicios";
            menu.MenuDetail = "Tipo de servicios";
            menu.Vista = "SeleccionTipoServiciosPage";
            menu.Imagen = "\uf03a";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Servicios registrados";
            menu.MenuDetail = "Servicios registrados";
            menu.Vista = "RegistroServiciosPage";
            menu.Imagen = "\uf0ce";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Historial de pagos";
            menu.MenuDetail = "Historial de pagos";
            menu.Vista = "HistorialBusquedaPage";
            menu.Imagen = "\uf073";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.ColorFondo = "#FFFFFF";
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Cerrar Sesión";
            menu.MenuDetail = "Cerrar Sesión";
            menu.Vista = "PantallaInicialPage";
            menu.Imagen = "\uf023";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = true;
            lista.Add(menu);

            return lista;
        }
    }
}
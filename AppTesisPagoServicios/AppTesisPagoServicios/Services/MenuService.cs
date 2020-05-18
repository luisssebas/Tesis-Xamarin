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
            menu.Imagen = "\uf0ce";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Servicio";
            menu.MenuDetail = "Servicio";
            menu.Vista = "ServicioAdminPage";
            menu.Imagen = "\uf0d6";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Parametrizaciones";
            menu.MenuDetail = "Parametrizaciones";
            menu.Vista = "ParametrizacionPage";
            menu.Imagen = "\uf080";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = false;
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Reportes";
            menu.MenuDetail = "Reportes";
            menu.Vista = "ReporteConsultaPage";
            menu.Imagen = "\uf080";
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
            menu.Imagen = "\uf0a8";
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
            menu.Imagen = "\uf0ce";
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
            menu.ColorFondo = "#FFFFFF";
            lista.Add(menu);

            menu = new MenuMD();
            menu.MenuTitle = "Cerrar Sesión";
            menu.MenuDetail = "Cerrar Sesión";
            menu.Vista = "PantallaInicialPage";
            menu.Imagen = "\uf0a8";
            menu.ColorFondo = "#FFFFFF";
            menu.Color = "#4D4D4D";
            menu.EsAdmin = true;
            menu.EsUsuario = true;
            lista.Add(menu);

            return lista;
        }
    }
}
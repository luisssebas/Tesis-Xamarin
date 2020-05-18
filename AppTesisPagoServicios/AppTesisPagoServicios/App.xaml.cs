using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTesisPagoServicios.Views;
using Prism.Unity;
using Prism;
using Prism.Ioc;
using Acr.UserDialogs;
using AppTesisPagoServicios.Services;

namespace AppTesisPagoServicios
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }
        public App(IPlatformInitializer initializer = null): base(initializer) { }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("MenuPage/NavigationPage/PantallaInicialPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            containerRegistry.RegisterInstance<IServicioUsuario>(new ServicioUsuario());

            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<PantallaInicialPage, PantallaInicialPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<AdministradorPage>();
            containerRegistry.RegisterForNavigation<TipoServicioPage>();
            containerRegistry.RegisterForNavigation<TipoReferenciaPage>();
            containerRegistry.RegisterForNavigation<TipoPagoPage>();
            containerRegistry.RegisterForNavigation<CatalogosTabbedPage, CatalogosPageViewModel>();
            containerRegistry.RegisterForNavigation<ServicioAdminPage, ServicioAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<ServicioEditarAdminPage, ServicioEditarAdminPageViewModel>();
            containerRegistry.RegisterForNavigation<RegistroUsuarioPage, RegistroUsuarioPageViewModel>();
            containerRegistry.RegisterForNavigation<SeleccionTipoServiciosPage, SeleccionTipoServiciosPageViewModel>();
            containerRegistry.RegisterForNavigation<ListaServiciosPage, ListaServiciosPageViewModel>();
            containerRegistry.RegisterForNavigation<ParametrizacionPage, ParametrizacionPageViewModel>();
            containerRegistry.RegisterForNavigation<ReporteConsultaPage, ReporteConsultaPageViewModel>();
            containerRegistry.RegisterForNavigation<InformacionPagoPage, InformacionPagoPageViewModel>();
        }


    }
}

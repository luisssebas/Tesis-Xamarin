using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTesisPagoServicios.Services;
using AppTesisPagoServicios.Views;
using Prism.Unity;
using Prism;
using Prism.Ioc;

namespace AppTesisPagoServicios
{
    public partial class App : PrismApplication
    {

        public App(IPlatformInitializer initializer = null): base(initializer) { }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("/PantallaInicialPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PantallaInicialPage, PantallaInicialPageViewModel>();
        }

    }
}

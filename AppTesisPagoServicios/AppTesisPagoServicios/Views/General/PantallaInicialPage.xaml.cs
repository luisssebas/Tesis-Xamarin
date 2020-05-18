using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTesisPagoServicios.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantallaInicialPage : ContentPage
    {
        public PantallaInicialPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
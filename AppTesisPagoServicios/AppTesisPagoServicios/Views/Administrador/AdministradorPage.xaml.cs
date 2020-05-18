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
    public partial class AdministradorPage : ContentPage
    {
        public AdministradorPage()
        {
            InitializeComponent();
        }

        private void OnclickedMenu(object sender, EventArgs e)
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = true;
        }
    }
}
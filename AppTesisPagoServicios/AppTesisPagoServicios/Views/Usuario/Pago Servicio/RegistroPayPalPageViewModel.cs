using Acr.UserDialogs;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesisPagoServicios.Views.Usuario.Pago_Servicio
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class RegistroPayPalPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INavigationService _navigationService;
        private readonly IServicioUsuario _servicioUsuario;

        public DelegateCommand PayPalCmd { get; set; }

        public RegistroPayPalPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IServicioUsuario servicioUsuario)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioUsuario = servicioUsuario;

            PayPalCmd = new DelegateCommand(PayPalEjecutar);
        }

        private async void PayPalEjecutar()
        {
            try
            {
                var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Test Product", new Decimal(12.50), "MXN"),
                    new Decimal(0));
                if (result.Status == PayPalStatus.Cancelled)
                {
                    await _userDialogs.AlertAsync("Cancelado");
                }
                else if (result.Status == PayPalStatus.Error)
                {
                    await _userDialogs.AlertAsync(result.ErrorMessage);
                }
                else if (result.Status == PayPalStatus.Successful)
                {
                    await _userDialogs.AlertAsync(result.ServerResponse.Response.Id);
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}

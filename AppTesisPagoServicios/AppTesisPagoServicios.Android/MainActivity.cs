using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Acr.UserDialogs;
using Prism.Ioc;
using PayPal.Forms.Abstractions;
using PayPal.Forms;
using Android.Content;

namespace AppTesisPagoServicios.Droid
{
    [Activity(Label = "AppTesisPagoServicios", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            UserDialogs.Init(() => this);

            var config = new PayPalConfiguration(PayPalEnvironment.NoNetwork, "EK1qvZuqKlUa0wx3Ioxii3WpXrgeSt7ZD1MoDT_KX4dINBWCq5WF5ou6eRAS1WwqsQxnhtYjxrN3DnB6")
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "One Pay",
                MerchantPrivacyPolicyUri = "https://www.example.com/privacy",
                MerchantUserAgreementUri = "https://www.example.com/legal",
                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                //ShippingAddressOption = ShippingAddressOption.Both,
                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "es",
                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "593998800091",
            };

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
            }
        }
    }
}
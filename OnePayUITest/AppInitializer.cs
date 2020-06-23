using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace OnePayUITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                //return ConfigureApp.Android.StartApp();
                return ConfigureApp.Android.InstalledApp("com.LMSistemas.OnePay").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}
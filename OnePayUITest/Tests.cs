using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace OnePayUITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        //[Test]
        //public void WelcomeTextIsDisplayed()
        //{
        //    AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
        //    app.Screenshot("Welcome screen.");

        //    Assert.IsTrue(results.Any());
        //}

        [Test]
        public void LoginSuccesfullTest()
        {
            app.WaitForElement(c => c.Marked("Acceder"));
            app.Tap(c => c.Marked("Acceder"));
            app.WaitForElement(c => c.Marked("entUsuario"));
            app.WaitForElement(c => c.Marked("entUsuario"));
            app.EnterText("entUsuario", "luissebas");
            app.EnterText("entPassword", "Sebas123*-");
            app.DismissKeyboard();
            app.WaitForElement(c => c.Marked("Ingresar"));
            app.Tap(c => c.Marked("Ingresar"));
        }
        
        [Test]
        public void RegisterSuccesfullTest()
        {
            app.WaitForElement(c => c.Marked("Acceder"));
            app.Tap(c => c.Marked("Acceder"));
            app.WaitForElement(c => c.Marked("Registrar"));
            app.Tap(c => c.Marked("Registrar"));
            app.EnterText("entIdentificacion", "1723730014");
            app.EnterText("entNombre", "Juana Estrella");
            app.EnterText("entCorreo", "luis_sebastian_ldu@hotmail.com");
            app.DismissKeyboard();
            app.ScrollDown("Contraseña");
            app.ScrollDown("Contraseña");
            app.ScrollDown("Contraseña");
            app.ScrollDown("Contraseña");
            app.ScrollDown("Contraseña");
            app.EnterText("entUsuario", "Juana27");
            app.DismissKeyboard();
            app.EnterText("entContrasenia", "Juana123");
            app.DismissKeyboard();
            app.EnterText("entComprobacionContrasenia", "Juana123");
            app.ScrollDown("Repita su contraseña");
            app.ScrollDown("Repita su contraseña");
            app.ScrollDown("Repita su contraseña");
            app.ScrollDown("Repita su contraseña");
            app.ScrollDown("Repita su contraseña");
            app.DismissKeyboard();
            app.Tap(c => c.Marked("Registrar"));
        }

        [Test]
        public void RegistrarServicio()
        {
            app.WaitForElement(c => c.Marked("Acceder"));
            app.Tap(c => c.Marked("Acceder"));
            app.WaitForElement(c => c.Marked("entUsuario"));
            app.WaitForElement(c => c.Marked("entUsuario"));
            app.EnterText("entUsuario", "luissebas");
            app.EnterText("entPassword", "Sebas123*-");
            app.DismissKeyboard();
            app.WaitForElement(c => c.Marked("Ingresar"));
            app.Tap(c => c.Marked("Ingresar"));
            app.WaitForElement(c => c.Marked("GrdBotones"));
            app.SwipeLeftToRight(0.99);
            app.Tap(c => c.Marked("Servicios registrados"));
            app.WaitForElement(c => c.Marked("btnAgregar"));
            app.Tap(c => c.Marked("btnAgregar"));
            app.WaitForElement(c => c.Marked("pkTipoServicio"));
            app.Tap(c => c.Marked("pkTipoServicio"));
            app.Tap(c => c.Marked("Teléfono"));
            app.Tap(c => c.Marked("pkServicio"));
            app.Tap(c => c.Marked("CNT"));
            app.WaitForElement(c => c.Marked("entCotrapartida"));
            app.ScrollDown("entCotrapartida");
            app.EnterText("entCotrapartida", "022558102");
            app.Tap(c => c.Marked("Guardar"));
        }
    }
}

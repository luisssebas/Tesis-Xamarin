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
    public partial class OTPPage : ContentPage
    {
        public string valorPosicion1 { get; set; }
        public string valorPosicion2 { get; set; }
        public string valorPosicion3 { get; set; }
        public string valorPosicion4 { get; set; }
        public string valorPosicion5 { get; set; }
        public string valorPosicion6 { get; set; }
        public string valorPosicion7 { get; set; }
        public string valorPosicion8 { get; set; }
        public string valorPosicion9 { get; set; }
        public string valorPosicion0 { get; set; }

        public OTPPage()
        {
            InitializeComponent();
            int minutos = 5, segundos = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (minutos == 0 && segundos == 0)
                {
                    Navigation.PopAsync();
                    return false;
                }
                else
                {
                    if (segundos == 0)
                    {
                        segundos = 59;
                        minutos--;
                    }
                    else
                    {
                        segundos--;
                    }
                }
                Device.BeginInvokeOnMainThread(() =>
                timer.Text = minutos.ToString() + ":" + segundos.ToString("00")
                );
                return true;
            });

            Posicion1.Clicked += Posicion1_Clicked;
            Posicion2.Clicked += Posicion2_Clicked;
            Posicion3.Clicked += Posicion3_Clicked;
            Posicion4.Clicked += Posicion4_Clicked;
            Posicion5.Clicked += Posicion5_Clicked;
            Posicion6.Clicked += Posicion6_Clicked;
            Posicion7.Clicked += Posicion7_Clicked;
            Posicion8.Clicked += Posicion8_Clicked;
            Posicion9.Clicked += Posicion9_Clicked;
            Posicion0.Clicked += Posicion0_Clicked;
            Limpiar.Clicked += Limpiar_Clicked;
            ValorOTP.Text = "";
            Ordenar();
        }

        private void Limpiar_Clicked(object sender, EventArgs e)
        {
            ValorOTP.Text = "";
        }

        private void Posicion9_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion9;
            }
        }


        private void Posicion8_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion8;
            }
        }

        private void Posicion7_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion7;
            }
        }

        private void Posicion6_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion6;
            }
        }

        private void Posicion5_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion5;
            }
        }

        private void Posicion4_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion4;
            }
        }

        private void Posicion3_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion3;
            }
        }

        private void Posicion2_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion2;
            }
        }

        private void Posicion1_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion1;
            }
        }
        private void Posicion0_Clicked(object sender, EventArgs e)
        {
            if (ValorOTP.Text.Length < 6)
            {
                ValorOTP.Text += valorPosicion0;
            }
        }

        private void Ordenar()
        {
            int columnas = 3;
            int filas = 3;
            int maxvalues = filas * columnas;
            HashSet<Int32> numeros = new HashSet<Int32>();
            int[] valores = new int[10];
            Random ran = new Random();

            while (numeros.Count < maxvalues + 1)
            {
                numeros.Add(ran.Next(maxvalues + 1));
            }
            numeros.CopyTo(valores);
            Posicion1.Text = "" + ValidarSource(valores[0])[1] + "";
            valorPosicion1 = ValidarSource(valores[0])[1];
            Posicion2.Text = "" + ValidarSource(valores[1])[1] + "";
            valorPosicion2 = ValidarSource(valores[1])[1];
            Posicion3.Text = "" + ValidarSource(valores[2])[1] + "";
            valorPosicion3 = ValidarSource(valores[2])[1];
            Posicion4.Text = "" + ValidarSource(valores[3])[1] + "";
            valorPosicion4 = ValidarSource(valores[3])[1];
            Posicion5.Text = "" + ValidarSource(valores[4])[1] + "";
            valorPosicion5 = ValidarSource(valores[4])[1];
            Posicion6.Text = "" + ValidarSource(valores[5])[1] + "";
            valorPosicion6 = ValidarSource(valores[5])[1];
            Posicion7.Text = "" + ValidarSource(valores[6])[1] + "";
            valorPosicion7 = ValidarSource(valores[6])[1];
            Posicion8.Text = "" + ValidarSource(valores[7])[1] + "";
            valorPosicion8 = ValidarSource(valores[7])[1];
            Posicion9.Text = "" + ValidarSource(valores[8])[1] + "";
            valorPosicion9 = ValidarSource(valores[8])[1];
            Posicion0.Text = "" + ValidarSource(valores[9])[1] + "";
            valorPosicion0 = ValidarSource(valores[9])[1];
        }
        private string[] ValidarSource(int valor)
        {
            string[] ValorTexto = new string[2];
            switch (valor)
            {
                case 1:
                    ValorTexto[0] = "uno";
                    ValorTexto[1] = "1";
                    break;
                case 2:
                    ValorTexto[0] = "dos";
                    ValorTexto[1] = "2";
                    break;
                case 3:
                    ValorTexto[0] = "tres";
                    ValorTexto[1] = "3";
                    break;
                case 4:
                    ValorTexto[0] = "cuatro";
                    ValorTexto[1] = "4";
                    break;
                case 5:
                    ValorTexto[0] = "cinco";
                    ValorTexto[1] = "5";
                    break;
                case 6:
                    ValorTexto[0] = "seis";
                    ValorTexto[1] = "6";
                    break;
                case 7:
                    ValorTexto[0] = "siete";
                    ValorTexto[1] = "7";
                    break;
                case 8:
                    ValorTexto[0] = "ocho";
                    ValorTexto[1] = "8";
                    break;
                case 9:
                    ValorTexto[0] = "nueve";
                    ValorTexto[1] = "9";
                    break;
                case 0:
                    ValorTexto[0] = "cero";
                    ValorTexto[1] = "0";
                    break;
            }
            return ValorTexto;
        }
    }
}
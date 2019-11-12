using Kursova;
using Loginning.Models;
using Loginning.Pages;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Loginning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            frame.Navigate(new Registration());

        }

        private void CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Log.Text = null;
            Pas.Password = null;
        }

        private void CloseProgram_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginViewModel info = new LoginViewModel();
                info.Login = Log.Text;
                info.Password = Pas.Password;
                string token;
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44395/api/UserAccount/login");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    Writer.Write(JsonConvert.SerializeObject(info));
                }

                WebResponse webResponse = httpWebRequest.GetResponse();

                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    token = reader.ReadToEnd();
                }

                if (token != null)
                {
                    var writer = new StreamWriter(System.IO.File.OpenWrite(@"..\..\ForTokens.txt"));
                    writer.WriteLine($"{token}");
                    writer.Close();

                    MainMainWindow win2 = new MainMainWindow();
                    win2.Show();
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

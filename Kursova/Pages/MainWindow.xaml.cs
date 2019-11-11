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

namespace Kursova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try {
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44395/api/UserAccount/register");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    Writer.Write(JsonConvert.SerializeObject(info));
                }

                WebResponse response = httpWebRequest.GetResponse();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

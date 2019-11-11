using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for MainMainWindow.xaml
    /// </summary>
    public partial class MainMainWindow : Window
    {
        public MainMainWindow()
        {
            InitializeComponent();
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44396/api/post/get");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";

                WebResponse response = httpWebRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    List<ProductViewModel> list = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
                    foreach (var item in list)
                    {
                        item.Image = "https://localhost:44396/api/content/ProductImages/" + item.Image;
                    }
                    dgProducts.ItemsSource = list;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }
}

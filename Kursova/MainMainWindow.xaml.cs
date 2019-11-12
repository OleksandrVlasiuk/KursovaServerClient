using API3.Models;
using Kursova.Pages;
using Loginning.Pages;
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
            frame.Navigate(new EditButton());
            try
            {
                string token;
                using (StreamReader Writer = new StreamReader("../../../Loginning/ForTokens.txt"))
                {
                    token = Writer.ReadLine();
                }
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44396/api/post/get");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer ");

                WebResponse response = httpWebRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    List<PostModel> list = JsonConvert.DeserializeObject<List<PostModel>>(json);
                    foreach (var item in list)
                    {
                        item.File = "https://localhost:44396/api/content/ProductImages/" + item.File;
                    }
                    AllPosts.ItemsSource = list;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

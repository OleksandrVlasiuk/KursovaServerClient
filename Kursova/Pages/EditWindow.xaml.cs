using API3.Models;
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

namespace Kursova.Pages
{
    /// <summary>
    /// Interaction logic for EditWindow2.xaml
    /// </summary>
    public partial class EditWindow2 : Page
    {
        public EditWindow2()
        {
            InitializeComponent();
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
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");

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
                //////////////////////////////////////////////
                editInnerAccountViewModel MyInfo = new editInnerAccountViewModel();
                HttpWebRequest httpWebRequest2 = WebRequest.CreateHttp("https://localhost:44396/api/UserAccount/OutputAccount");
                httpWebRequest2.Method = "GET";
                httpWebRequest2.ContentType = "application/json";
                httpWebRequest2.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
                WebResponse response2 = httpWebRequest2.GetResponse();
                using (StreamReader reader = new StreamReader(response2.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    MyInfo = JsonConvert.DeserializeObject<editInnerAccountViewModel>(json);
                }
                MainName.DataContext = MyInfo;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            //try {
            //    HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44395/api/UserAccount/register");
            //    httpWebRequest.Method = "POST";
            //    httpWebRequest.ContentType = "application/json";
            //    using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            //    {
            //        Writer.Write(JsonConvert.SerializeObject(info));
            //    }

            //    WebResponse response = httpWebRequest.GetResponse();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.Message);
            //}
        }

        private void CloseProgram_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}

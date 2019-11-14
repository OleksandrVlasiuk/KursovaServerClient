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

                List<PostModel> list;
                string token;
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\ForTokens.txt"))
                {
                    token = reader.ReadLine();
                }
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://localhost:2202/api/post/get");
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");

                WebResponse response = httpWebRequest.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<PostModel>>(json);
                    foreach (var item in list)
                    {
                        item.File = "http://localhost:2202/api/content/ProductImages/" + item.File;
                    }

                }
                AllPosts.ItemsSource = list;
                //////////////////////////////////////////////
                HttpWebRequest httpWebRequest2 = WebRequest.CreateHttp("http://localhost:2202/api/UserAccount/OutputAccount");
                httpWebRequest2.Method = "GET";
                httpWebRequest2.ContentType = "application/json";
                httpWebRequest2.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");

                WebResponse response2 = httpWebRequest2.GetResponse();

                using (StreamReader reader = new StreamReader(response2.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    editInnerAccountViewModel Me = JsonConvert.DeserializeObject<editInnerAccountViewModel>(json);
                    Me.Image = "http://localhost:2202/api/content/ProductImages/" + Me.Image;
                    MainName.Text = Me.Name;
                    if (Me.Image != null) Me.Image = "Assets / NoImage.png";
                    MainName.Text = Me.Login;
                    MyIcon.DataContext = Me.Image;
                    em.Text = Me.Email;
                    ph.Text = Me.PhoneNumber;
                    na.Text = Me.Name;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void CloseProgram_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string image64 = Image64.Text;
            int likes = 0;
            string MyCom = null;
            PostModel info = new PostModel();
            info.File = image64;
            info.Likes = likes;
            info.MyComment = MyCom;
            string token;
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\ForTokens.txt"))
            {
                token = reader.ReadLine();
            }
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://localhost:2202/api/post/AddPost");
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
            using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Writer.Write(JsonConvert.SerializeObject(info));
            }
            WebResponse response = httpWebRequest.GetResponse();
            MessageBox.Show("Successfuly added");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            editInnerAccountViewModel info = new editInnerAccountViewModel();
            info.Name = na.Text;
            info.Email = em.Text;
            info.PhoneNumber = ph.Text;
            string token;
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\ForTokens.txt"))
            {
                token = reader.ReadLine();
            }
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp("http://localhost:2202/api/UserAccount/EditAccount");
            httpWebRequest.Method = "PUT";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
            using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Writer.Write(JsonConvert.SerializeObject(info));
            }
            WebResponse response = httpWebRequest.GetResponse();
            MessageBox.Show("Successfuly changed");



        }
    }
}

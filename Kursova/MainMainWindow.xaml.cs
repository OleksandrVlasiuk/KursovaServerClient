using API3.Models;
using Kursova.Pages;
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
                List<PostModel> list;
                string token;
                using (StreamReader reader = new StreamReader(Environment.CurrentDirectory+@"\ForTokens.txt"))
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
                ////////////////////////////////
                HttpWebRequest httpWebRequest3 = WebRequest.CreateHttp("http://localhost:2202/api/friends/getfriend");
                httpWebRequest3.Method = "GET";
                httpWebRequest3.ContentType = "application/json";
                httpWebRequest3.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
                List<editInnerAccountViewModel> friendModels ;
                WebResponse response3 = httpWebRequest3.GetResponse();
                using (StreamReader reader = new StreamReader(response3.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    friendModels = JsonConvert.DeserializeObject<List<editInnerAccountViewModel>>(json);
                }

                ////////////////////////////////////  
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
                    MainImage.DataContext = Me;
                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChatWindow win2 = new ChatWindow();
            win2.Show();
        }

        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PackIcon packIcon = (PackIcon)sender;
            

        }
    }
}

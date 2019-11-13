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
                using (StreamReader reader = new StreamReader("../../../Loginning/ForTokens.txt"))
                {
                    token = reader.ReadLine();
                }
                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44396/api/post/get");
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
                        item.File = "https://localhost:44396/api/content/ProductImages/" + item.File;
                    }
                    
                }
                    AllPosts.ItemsSource = list;

                HttpWebRequest httpWebRequest3 = WebRequest.CreateHttp("https://localhost:44396/api/friend/getfriend");
                httpWebRequest3.Method = "GET";
                httpWebRequest3.ContentType = "application/json";
                httpWebRequest3.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
                List<FriendModel> friendModels ;
                List<editInnerAccountViewModel> fri = new List<editInnerAccountViewModel>();
                WebResponse response3 = httpWebRequest3.GetResponse();
                using (StreamReader reader = new StreamReader(response3.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    friendModels = JsonConvert.DeserializeObject<List<FriendModel>>(json);
                    foreach (var item in friendModels)
                    {
                        fri.Add();
                    }

                }
                AllPosts.ItemsSource = list;
                /////////////////////////////////////////////
                List<FriendModel> friends;
                List<string> fr = new List<string>();
                HttpWebRequest httpWebRequest2 = WebRequest.CreateHttp("https://localhost:44396/api/friends/getfriend");
                httpWebRequest2.Method = "GET";
                httpWebRequest2.ContentType = "application/json";
                httpWebRequest2.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");

                WebResponse response2 = httpWebRequest2.GetResponse();
                using (StreamReader reader = new StreamReader(response2.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    friends = JsonConvert.DeserializeObject<List<FriendModel>>(json);                   
                }
                foreach (var t in friends)
                {
                    fr.Add(t.UserAccount_id);
                }

                HttpWebRequest httpWebRequest3 = WebRequest.CreateHttp("https://localhost:44396/api/UserAccount/getIcons");
                httpWebRequest3.Method = "GET";
                httpWebRequest3.ContentType = "application/json";
                httpWebRequest3.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
                
                using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    Writer.Write(JsonConvert.SerializeObject(fr));
                }

                WebResponse response3 = httpWebRequest2.GetResponse();

                using (StreamReader reader = new StreamReader(response3.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();
                    List<editInnerAccountViewModel> list = JsonConvert.DeserializeObject<List<editInnerAccountViewModel>>(json);
                    foreach (var item in list)
                    {

                        item.Image = "https://localhost:44396/api/content/ProductImages/" + item.Image;
                    }
                    AllPosts.ItemsSource = list;
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

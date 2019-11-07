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

namespace Loginning.Pages
{
    /// <summary>
    /// Interaction logic for Creating.xaml
    /// </summary>
    public partial class Creating : Page
    {
        public Creating()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Add Account";
            Title.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6A10A8"));
            this.NavigationService.GoBack();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            try {
                AddAccountViewModel info = new AddAccountViewModel();
                info.Login = Login.Text;
                info.Password = Password.Text;
                info.Name = Name.Text;
                info.TelephoneNumber = Name.Text;
                info.Email = Email.Text;

                HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44395/api/UserAccount/register");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                using (StreamWriter Writer = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    Writer.Write(JsonConvert.SerializeObject(info));
                }

                WebResponse response = httpWebRequest.GetResponse();

                Title.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1A8733"));
                Title.Text = "Successfully";
            }
            catch (Exception ex) {
                Title.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB20920"));
                Title.Text = "Bad data";
                MessageBox.Show(ex.InnerException.Message); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login.Text = null;
            Password.Text = null;
            Email.Text = null;
            Phone.Text = null;
            Name.Text = null;
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
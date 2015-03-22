using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsPhone_TokenBasedAuth.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace WindowsPhone_TokenBasedAuth
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TokenService tokenService = null;
        private AuthService authService = null;
        private HelloService helloService = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

   
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            tokenService = new TokenService();
            authService = new AuthService();
            helloService = new HelloService();

            if (tokenService.IsTokenExist())
            {
                InitializeWelcomeUIElements();
            }

        }

        private async void btnGetToken_Click(object sender, RoutedEventArgs e)
        {
            var token = await authService.GetAccessToken(txtUsername.Text, txtPassword.Password);


            //in other words -> if the username and password is valid
            if (token.AccessToken != null)
            {
                //Save the token to Local Storage
                tokenService.SaveToken(token);

                InitializeWelcomeUIElements();
            }

            else
            {
                tbStatus.Text = "Check your credentials.";
            }

        }

        void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            //Delete the token from local storage
            tokenService.DeleteToken();

            InitializeLoginUIElements();
        }

        private async void btnCallAPI_Click(object sender, RoutedEventArgs e)
        {
            if (tokenService.IsTokenExist())
                tbStatus.Text = await helloService.Get(tokenService.GetToken());
            else

                tbStatus.Text = "There is no token in the local storage";

        }
        
        private void InitializeWelcomeUIElements()
        {
            tbStatus.Text = String.Format("Welcome, {0}", tokenService.GetToken().Username);

            stkLogin.Children.Clear();

            Button btnLogout = new Button()
            {
                Content = "Logout",
                Name = "btnLogout",
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            btnLogout.Click += btnLogout_Click;

            stkLogin.Children.Add(btnLogout);
        }


        private void InitializeLoginUIElements()
        {
            tbStatus.Text = "You have logged out.";
            stkLogin.Children.Clear();

            stkLogin.Children.Add(tbUsername);
            stkLogin.Children.Add(txtUsername);
            stkLogin.Children.Add(tbPassword);
            stkLogin.Children.Add(txtPassword);
            stkLogin.Children.Add(btnGetToken);
        }

      

   
    }
}

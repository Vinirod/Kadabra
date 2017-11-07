using Android.Content.Res;
using Kadabra.Helpers;
using Kadabra.Services;
using Kadabra.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kadabra.ViewModels
{
    public class LoginViewModel
    {
       
        private ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }
        public string Password { get; set; }
        public bool isLogin;

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                    
                    var accessToken = await _apiServices.LoginAsync(Username, Password);
                    Settings.AccessToken = accessToken;

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new MenuPage());
                    }
                    else if(string.IsNullOrEmpty(accessToken)){
                        await App.Current.MainPage.DisplayAlert("Algo errado", "Email ou senha errado", "Ok");
                    }
                });
            }
        }

        public LoginViewModel()
        {
            Username = Settings.UserName;
            Password = Settings.Password;

        }
    }
}

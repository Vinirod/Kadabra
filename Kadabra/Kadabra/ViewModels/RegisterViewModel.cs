using Kadabra.Helpers;
using Kadabra.Services;
using Kadabra.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Kadabra.ViewModels
{
    public class RegisterViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async() =>
                {
                    /*
                    var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

                    if(Regex.IsMatch(Email, emailPattern))
                        await App.Current.MainPage.DisplayAlert("Algo Errado", "Padrão de email inválido", "Ok");
                    else {
                    */
                        var isSuccess = await _apiServices.RegisterAsync(Email, Password, ConfirmPassword);

                        if (isSuccess)
                        {
                            Settings.UserName = Email;
                            Settings.Password = Password;
                            Message = "Registrado com Sucesso";
                            await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                        }
                        /*else if(Email.Equals("") == true)
                            await App.Current.MainPage.DisplayAlert("Algo Errado", "Campo em branco", "Ok");*/
                        else
                            await App.Current.MainPage.DisplayAlert("Algo Errado", "Esse email já foi cadastrado", "Ok");
                    //}
                });
            }   
        }
    }
}

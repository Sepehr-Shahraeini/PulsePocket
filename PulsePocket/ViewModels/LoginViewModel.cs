using Microsoft.Maui.Controls;
using System;

namespace PulsePocket.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string userName;
        string password;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();
        }


        public string UserName
        {
            get => this.userName;
            set => SetProperty(ref this.userName, value);
        }

        public string Password
        {
            get => this.password;
            set => SetProperty(ref this.password, value);
        }

        public Command LoginCommand { get; }


        async void OnLoginClicked()
        {
            await Navigation.NavigateToAsync<HomeViewModel>(true);
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            //Shell.Current.ToolbarItems.Add(LogOutTool);
        }

        bool ValidateLogin()
        {
            return !String.IsNullOrWhiteSpace(UserName)
                && !String.IsNullOrWhiteSpace(Password);
        }
    }
}
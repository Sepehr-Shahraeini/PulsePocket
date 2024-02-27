using Microsoft.Maui.Platform;
using PulsePocket.Models;
using PulsePocket.ViewModels;
using System.Runtime.CompilerServices;

namespace PulsePocket.Views
{
    public partial class MainPage : Shell
    {


        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();

            Routing.RegisterRoute("LibraryPage", typeof(LibraryPage));

            
            

        }


       

        public void SwitchtoTab(int tabIndex)
        {
            
            Device.BeginInvokeOnMainThread(() =>
            {
                switch (tabIndex)
                {
                    case 0: tabbar.CurrentItem = tab0; break;
                    case 1: { tabbar.CurrentItem = tab1; Globals.Index = "1"; } break;
                    case 2: tabbar.CurrentItem = tab2; break;
                    case 3: tabbar.CurrentItem = tab3; break;
                    case 4: { tabbar.CurrentItem = tab4; Globals.Index = "4"; } break;
                };
            });
        }

        private async void LogOutClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
            {
                SecureStorage.RemoveAll();
                await Shell.Current.GoToAsync($"///{typeof(LoginPage)}");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
                Shell.Current.FlyoutIsPresented = false;

            }
        }

        private void tab1_Appearing(object sender, EventArgs e)
        {
            Globals.Index = "1";
        }

        private void tab3_Appearing(object sender, EventArgs e)
        {
            Globals.Index = "3";
        }
        private void tab4_Appearing(object sender, EventArgs e)
        {
            Globals.Index = "4";
        }

     
    }
}
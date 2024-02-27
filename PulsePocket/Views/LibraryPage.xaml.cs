using DevExpress.Maui.Pdf;
using PulsePocket.ViewModels;

namespace PulsePocket.Views;

public partial class LibraryPage : ContentPage
{
	public LibraryPage()
	{
        BindingContext = ViewModel = new ItemsViewModel();
        InitializeComponent();
	}

    ItemsViewModel ViewModel { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.OnAppearing();
    }

    
    private async void Search(object sender, EventArgs e)
    {
        ViewModel.PerformSearch(((InputView)sender).Text);
    }

}



using PulsePocket.Models;
using Syncfusion.Maui.Core.Carousel;

namespace PulsePocket.Views;


public partial class FlightsPage : ContentPage
{
    public class UserInfo
    {
        public string userName { get; set; }
        public string password { get; set; }
        public int userId { get; set; }
        public int employeeId { get; set; }

        public string Position { get; set; }
        public string TIndex { get; set; }   
    }

    public FlightsPage()
    {
        InitializeComponent();
#if DEBUG
        FlightsWebView.EnableWebDevTools = true;

#endif

        FlightsWebView.JSInvokeTarget = new MyJSInvokeTarget(this);

    }


    UserInfo userInfo = new UserInfo();

    protected override void OnAppearing()
    {
       
        userInfo.userName = "dehghan";
        userInfo.password = "Magu1359";
        userInfo.userId = 238;
        userInfo.employeeId = 232;
        userInfo.Position = "P1";
        userInfo.TIndex = Globals.Index;

        CallJs("test");
    
    }


    //private async void HybridWebView_RawMessageReceived(object sender, HybridWebView.HybridWebViewRawMessageReceivedEventArgs e)
    //{
    //    await Dispatcher.DispatchAsync(async () =>
    //    {
    //        await DisplayAlert("JavaScript message", e.Message, "OK");
    //    });
    //}


    //public async void Button_Clicked(object sender, EventArgs e)
    //{
    //    CallJs("login");
    //}

    public void CallJs(string s)
    {
      

        FlightsWebView.InvokeJsMethodAsync("GetUserInfo", userInfo);
    }


    


    private sealed class MyJSInvokeTarget
    {
        private FlightsPage _flightsPage;

        public MyJSInvokeTarget(FlightsPage flightsPage)
        {
            _flightsPage = flightsPage;
        }

        //public void CallMeFromScript(string message, int value)
        //{
        //    _flightsPage.CallJs("Hi!");
        //}
        void dool()
        {
            _flightsPage.CallJs("Hi!");
        }


        public void GetPageIndex()
        {
            if (MainThread.IsMainThread)
            {
                int i = 0;
            }

            else
            {
                MainThread.BeginInvokeOnMainThread(dool);

            }

            _flightsPage.CallJs("test");
        }

        public void CallMeFromJs(string s)
        {
            if (MainThread.IsMainThread)
            {
                int i = 0;
            }

            else
            {
                MainThread.BeginInvokeOnMainThread(dool);

            }
            //  MainThread.BeginInvokeOnMainThread(MyMainThreadCode);
            //_flightsPage.FlightsWebView.InvokeJsMethodAsync("ShowAlert", "Hello!");
            _flightsPage.CallJs("Hi!");

            //_flightsPage.btn_test.Text = "dool";
            var test = s;
        }
    }

   
}
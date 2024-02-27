using Android.App;
using Android.Runtime;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using PulsePocket.Platforms.Android;

namespace PulsePocket
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }
        protected override MauiApp CreateMauiApp()
        {


//            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CustomShellRenderer", (hander, view) =>
//            {
//#if ANDROID
//                hander.AddHandler(typeof(Shell), typeof(CustomShellRenderer));
//#endif
//            });


            return MauiProgram.CreateMauiApp();
        }
    }
}
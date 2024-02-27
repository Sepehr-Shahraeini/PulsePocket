using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Tabs;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AndroidApp = Android.App.Application;


namespace PulsePocket.Platforms.Android
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context)
        {

        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new ShellBottomNavViewAppearanceTrackerEx(this, shellItem.CurrentItem);
        }

    }
    class ShellBottomNavViewAppearanceTrackerEx : ShellBottomNavViewAppearanceTracker
    {
        private readonly IShellContext shellContext;

        public ShellBottomNavViewAppearanceTrackerEx(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
            this.shellContext = shellContext;
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);
            //var backgroundDrawable = new GradientDrawable();
            //backgroundDrawable.SetShape(ShapeType.Rectangle);
            //backgroundDrawable.SetCornerRadii(new float[] { 30, 30, 30, 30, 0, 0, 0, 0 });
            //bottomView.SetBackground(backgroundDrawable);
            bottomView.SetMinimumHeight(115);
        }
    }
}

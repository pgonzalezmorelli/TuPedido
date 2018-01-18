
using Android.App;
using Android.Views;
using Xamarin.Forms;

namespace TuPedido.Droid.Helpers
{
    public class StatusBarHelper : TuPedido.Helpers.IStatusBar
    {
        WindowManagerFlags _originalFlags;

        public void Hide()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        public void Show()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }
    }
}
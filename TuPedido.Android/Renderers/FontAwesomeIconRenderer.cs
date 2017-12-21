using Android.Content;
using Android.Graphics;
using TuPedido.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FontAwesomeIcon), typeof(TuPedido.Droid.Renderers.FontAwesomeIconRenderer))]
namespace TuPedido.Droid.Renderers
{
    public class FontAwesomeIconRenderer : LabelRenderer
    {
        public FontAwesomeIconRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Typeface = Typeface.CreateFromAsset(Context.Assets, $"fonts/{FontAwesomeIcon.Typeface}.ttf");
            }
        }
    }
}
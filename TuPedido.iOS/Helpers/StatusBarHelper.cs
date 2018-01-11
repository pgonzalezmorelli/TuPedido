using UIKit;

namespace TuPedido.iOS.Helpers
{
    public class StatusBarHelper : TuPedido.Helpers.IStatusBar
    {
        public void Hide()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void Show()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }
    }
}

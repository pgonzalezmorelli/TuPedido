
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuPedido.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationView : NavigationPage
    {
		public NavigationView ()
		{
			InitializeComponent ();
		}

        public NavigationView(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}
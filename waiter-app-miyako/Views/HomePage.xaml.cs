using waiter_app_miyako.ViewModels;

namespace waiter_app_miyako.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomePageViewModel();
        }
    }
}
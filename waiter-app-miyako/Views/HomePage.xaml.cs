using waiter_app_miyako.ViewModels;

namespace waiter_app_miyako.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly HomePageViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();
            _viewModel = new HomePageViewModel();
            this.BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CarregarMesas();
        }
    }
}
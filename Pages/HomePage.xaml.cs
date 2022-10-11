namespace TwoPoi;

public partial class HomePage : ContentPage
{
	private HomePageViewModel _homePageViewModel;

    public HomePage()
	{
		InitializeComponent();
        _homePageViewModel = new HomePageViewModel();
        BindingContext = _homePageViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _homePageViewModel.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _homePageViewModel.Dispose();
    }
}


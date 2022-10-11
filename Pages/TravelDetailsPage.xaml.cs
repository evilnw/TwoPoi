
namespace TwoPoi;

public partial class TravelDetailsPage : ContentPage
{
    private readonly TravelDetailsViewModel _travelDetailsViewModel;
    
    public TravelDetailsPage()
	{
		InitializeComponent();
        _travelDetailsViewModel = new TravelDetailsViewModel();
        BindingContext = _travelDetailsViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _travelDetailsViewModel.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _travelDetailsViewModel.Dispose();
    }
}
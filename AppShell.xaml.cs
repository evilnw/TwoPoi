namespace TwoPoi;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(TravelDetailsPage), typeof(TravelDetailsPage));
	}
}

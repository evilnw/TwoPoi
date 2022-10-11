using Microsoft.Maui.Devices.Sensors;

namespace TwoPoi;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
    
    protected override async void OnStart()
    {
        base.OnStart();
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status == PermissionStatus.Denied || status == PermissionStatus.Unknown)
        {
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }
    }

    protected override async void OnResume()
    {
        base.OnResume();
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status == PermissionStatus.Denied || status == PermissionStatus.Unknown)
        {
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }
    }
}

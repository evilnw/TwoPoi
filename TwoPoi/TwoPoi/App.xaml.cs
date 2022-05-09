using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TwoPoi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainPage = new MainPage();
            NavigationPage.SetHasNavigationBar(mainPage, false);
            NavigationPage.SetHasBackButton(mainPage, false);
            MainPage = mainPage;
        }

        protected override async void OnStart()
        {
            /*var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Denied || status == PermissionStatus.Unknown)
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }*/
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

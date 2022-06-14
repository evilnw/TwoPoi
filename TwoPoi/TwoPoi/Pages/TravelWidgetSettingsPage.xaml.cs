using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TwoPoi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelWidgetSettingsPage : ContentPage
    {
        private TravelWidgetSettings _travelWidgetSettings;
        
        public TravelWidgetSettingsPage(TravelWidgetSettings travelWidgetSettings)
        {
            InitializeComponent();
            _travelWidgetSettings = travelWidgetSettings;
            BindingContext = _travelWidgetSettings;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //_travelWidget.Dispose();
        }
    }
}
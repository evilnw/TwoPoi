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
    public partial class TravelWidgetPage : ContentPage
    {
        private TravelWidget _travelWidget;
        
        public TravelWidgetPage(TravelWidget travelWidget)
        {
            InitializeComponent();
            _travelWidget = travelWidget;
            BindingContext = _travelWidget;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _travelWidget.Dispose();
        }
    }
}
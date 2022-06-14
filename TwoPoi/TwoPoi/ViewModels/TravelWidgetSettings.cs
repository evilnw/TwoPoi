using System.ComponentModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TwoPoi
{
    public class TravelWidgetSettings : INotifyPropertyChanged
    {
        public TravelWidget TravelWidget { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Command OpenPoiNameEditorCommand { get; }
        public Command AcceptPoiNameCommand { get; }
        public Command SelectStyleCommand { get; }

        public TravelWidgetSettings(TravelWidget travelWidget, CurrentPosition currentPosition)
        {
            TravelWidget = travelWidget;
        }

        
    }
}

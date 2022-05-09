using System.ComponentModel;
using Xamarin.Forms;

namespace TwoPoi
{
    public class TravelWidget : INotifyPropertyChanged
    {
        private double _distance;
        
        public PointOfInterest PointOfInterest { get; }

        public TravelStyle TravelStyle { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public double Latitude => PointOfInterest.Latitude;

        public double Longitude => PointOfInterest.Longitude;

        public string Name
        {
            get => PointOfInterest.Name;
            set
            {
                PointOfInterest.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        /// <summary>
        /// Meters
        /// </summary>
        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Distance)));
            }
        }

        public Color TextColor => Color.FromHex(TravelStyle.TextHexColor);

        public Color BackgroundColor => Color.FromHex(TravelStyle.BackgroundHexColor);

        public Color DistanceBackgroundColor => Color.FromHex(TravelStyle.BackgroundHexColor);

        public TravelWidget(
            PointOfInterest pointOfInterest, 
            TravelStyle travelStyle)
        {
            PointOfInterest = pointOfInterest;
            TravelStyle = travelStyle;
        }
    }
}

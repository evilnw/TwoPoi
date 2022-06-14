using System.ComponentModel;
using Xamarin.Forms;

namespace TwoPoi
{
    public class CurrentPosition : INotifyPropertyChanged
    {
        private double _latitude;

        private double _longitude;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Latitude
        {
            get => _latitude;
            set
            {
                if (_latitude != value)
                {
                    _latitude = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Latitude)));
                }
            }
        }

        public double Longitude
        {
            get => _longitude;
            set
            {
                if (_longitude != value)
                {
                    _longitude = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Latitude)));
                }
            }
        }
    }
}

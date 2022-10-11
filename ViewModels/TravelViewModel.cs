using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TwoPoi;

public class TravelViewModel : INotifyPropertyChanged
{
    private double _distance = 0;

    public PointOfInterest Poi { get; }

    public Color Color => 
        Application.Current.Resources.TryGetValue(Poi.ColorType.ToString(), out var colorObject) ? colorObject as Color : new Color();

    public double Distance
    {
        get => _distance;
        set
        {
            _distance = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public TravelViewModel(PointOfInterest poi)
        => Poi = poi;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

    public void UpdateDistance(Location currentLocation)
    {
        if (currentLocation == null)
        {
            return;
        }
        Distance = Math.Round(Location.CalculateDistance(currentLocation, Poi.Latitude, Poi.Longitude, DistanceUnits.Kilometers), 3) * 1000;
    }
}

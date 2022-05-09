using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TwoPoi
{
    public enum Status { Ready, TravelCreation, Error, ConnectionProblem }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private Status _status = Status.Ready;
        private double _currentPosLatitude = 999;
        private double _currentPosLongitude = 999;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<TravelWidget> TravelWidgetsCollection { get; set; }

        public Command CreateTravelWidgetCommand { get; }
        
        public Command OpenTravelWidgetSettingsCommand { get; set; }

        public Status Status
        {
            get => _status;
            set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public double CurrentLatitude
        {
            get => _currentPosLatitude;
            set
            {
                _currentPosLatitude = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLatitude)));
            }
        }
        
        public double CurrentLongitude
        {
            get => _currentPosLongitude;
            set
            {
                _currentPosLongitude = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLongitude)));
            }
        }

        public MainPageViewModel()
        {
            TravelWidgetsCollection = new ObservableCollection<TravelWidget>();
            CreateTravelWidgetCommand = new Command(async () => await CreateTravelWidget());
            OpenTravelWidgetSettingsCommand = new Command(async (obj) => await OpenTravelWidgetSettings(obj));
        }

        public async Task CreateTravelWidget()
        {
            /*var poi = new PointOfInterest("test", 11, 22);
            var travelStyle = new TravelStyle(poi, "424b54", "EBCFB2", "788AA3");
            TravelWidgetsCollection.Insert(0, new TravelWidget(poi, travelStyle));*/
        }

        public async Task OpenTravelWidgetSettings(object travelWidgetObject)
        {

        }
    }
}

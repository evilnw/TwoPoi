using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TwoPoi.DataBase;

namespace TwoPoi;

public enum PoiCreationStatus { Ready, InProcess, Error };

public class HomePageViewModel : INotifyPropertyChanged, IDisposable
{
    private Timer _timer;

    private PoiCreationStatus _poiCreationStatus;

    public PoiCreationStatus PoiCreationStatus
    {
        get => _poiCreationStatus;
        set
        {
            _poiCreationStatus = value;
            OnPropertyChanged();
        }
    }

    public ICommand CreatePoiCommand { get; }

    public ICommand OpenTravelDetailsPageCommand { get; }

    public ObservableCollection<TravelViewModel> TravelsViewModels { get; set; } = new ObservableCollection<TravelViewModel>();

    public event PropertyChangedEventHandler PropertyChanged;

    public HomePageViewModel()
    {
        CreatePoiCommand = new Command(async () => await CreatePointOfInterest());
        OpenTravelDetailsPageCommand = new Command(async (obj) => await OpenTravelDetailsPage(obj));
    }

    public void Start()
    {
        TravelsViewModels.Clear();
        PoiCreationStatus = PoiCreationStatus.Ready;

        var repository = new Repository();
        var pointsOfInterest = repository.Items.Reverse();
        foreach (var poi in pointsOfInterest)
        {
            TravelsViewModels.Add(new TravelViewModel(poi));
        }

        _timer = new Timer(UpdateAsync, null, 1000, 10000);
    }

    public void Dispose()
    {
        _timer?.Dispose();
        TravelsViewModels.Clear();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void UpdateAsync(object data = null)
    {
        var currentLocation = await GetCurrentLocationAsync();
        if (currentLocation == null)
        {
            return;
        }

        foreach(var travelViewModel in TravelsViewModels.ToArray())
        {
            travelViewModel.UpdateDistance(currentLocation);
        }
    }

    private async Task OpenTravelDetailsPage(object travelViewModelObject)
    {
        var travelViewModel = travelViewModelObject as TravelViewModel;

        await Shell.Current.GoToAsync(nameof(TravelDetailsPage), 
            new Dictionary<string, object>()
            {
                {nameof(PointOfInterest), travelViewModel.Poi }
            });
    }

    private async Task CreatePointOfInterest()
    {
        if (PoiCreationStatus != PoiCreationStatus.Ready)
        {
            return;
        }
        
        PoiCreationStatus = PoiCreationStatus.InProcess;
        var currentLocation = await GetCurrentLocationAsync(15);
        
        if (currentLocation == null)
        {
            PoiCreationStatus = PoiCreationStatus.Error;
            await Task.Delay(1000);
            PoiCreationStatus = PoiCreationStatus.Ready;
            return;
        }
        
        var poi = new PointOfInterest() 
        { 
            Name = String.Empty, 
            Latitude = Math.Round(currentLocation.Latitude, 6),
            Longitude = Math.Round(currentLocation.Longitude, 6)
        };

        TravelsViewModels.Insert(0, new TravelViewModel(poi));

        var repository = new Repository();
        await repository.AddItemAsync(poi);
        PoiCreationStatus = PoiCreationStatus.Ready;
    }

    private async Task<Location?> GetCurrentLocationAsync(int timeoutSeconds = 10)
    {
        var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(timeoutSeconds));
        using var canceletionTokenSource = new CancellationTokenSource();
        try
        {
            return await Geolocation.GetLocationAsync(geolocationRequest, canceletionTokenSource.Token);
        }
        catch (Exception)
        {
            return null;
        }
    }
}

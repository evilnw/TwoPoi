using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TwoPoi.DataBase;

namespace TwoPoi;

public class TravelDetailsViewModel : INotifyPropertyChanged, IDisposable, IQueryAttributable
{
    #region Variables

	private Timer _timer;

    private PointOfInterest _poi;

    private Color _color;

    public TravelNagiationViewModel TravelNagiationViewModel { get; }

    public string Name
    {
        get => _poi?.Name;
        set
        {
            if (_poi == null)
                return;
            
            _poi.Name = value;
            OnPropertyChanged();
        }
    }

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand RenameCommand { get; }

    public ICommand SetColorCommand { get; }

    public ICommand СoordinatesToClipboardCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand ShowConfirmDeleteMenuCommand { get; }

    public ICommand CloseConfirmDeleteMenuCommand { get; }
    
    #endregion

    public TravelDetailsViewModel()
    {
        TravelNagiationViewModel = new TravelNagiationViewModel();
        RenameCommand = new Command(async (nameEntryObject) => await RenameAsync(nameEntryObject));
        SetColorCommand = new Command(async (colorButtonObject) => await SetColorAsync(colorButtonObject));
        СoordinatesToClipboardCommand = new Command(async () => await СoordinatesToClipboardAsync());
        DeleteCommand = new Command(async () => await DeleteAsync());
        ShowConfirmDeleteMenuCommand = new Command(
            (obj) => { (obj as VisualElement).IsEnabled = false; });
        CloseConfirmDeleteMenuCommand = new Command(
            (obj) => { (obj as VisualElement).IsEnabled = true; });
    }


    public event PropertyChangedEventHandler PropertyChanged;

    public void Start()
	{
		_timer = new Timer(UpdateDistanceAsync, null, 1000, 10000);
	}

	public void Dispose()
	{
		_timer.Dispose();
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _poi = query[nameof(PointOfInterest)] as PointOfInterest;
        Name = _poi.Name;
        Color = (Application.Current.Resources.TryGetValue(_poi.ColorType.ToString(), out var colorObject)) ? 
            colorObject as Color : throw new Exception($"Colors.xaml does not contain {_poi.ColorType.ToString()} color.");
        TravelNagiationViewModel.Latitude = _poi.Latitude;
        TravelNagiationViewModel.Longitude = _poi.Longitude;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void UpdateDistanceAsync(object obj = null)
	{
        var geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
        using var canceletionTokenSource = new CancellationTokenSource();
        try
        {
            var currentLocation = await Geolocation.GetLocationAsync(geolocationRequest, canceletionTokenSource.Token);
            
            if (currentLocation == null || _poi == null)
            {
                return;
            }

            TravelNagiationViewModel.Distance = Math.Round(Location.CalculateDistance(currentLocation, _poi.Latitude, _poi.Longitude, DistanceUnits.Kilometers), 3) * 1000;
        }
        catch (Exception)
        {
            return;
        }
    }

	private async Task BackToHomePage()
        => await Shell.Current.GoToAsync("..");

    private async Task RenameAsync(object nameEntryObject)
    {
        var nameEntry = nameEntryObject as Entry;
        if (nameEntry.IsReadOnly == true)
        {
            nameEntry.IsReadOnly = false;
            nameEntry.Focus();
        }
        else
        {
            nameEntry.IsReadOnly = true;
            nameEntry.Unfocus();
            var repository = new Repository();
            await repository.UpdateItemAsync(_poi);
        }
    }

    private async Task SetColorAsync(object colorButtonObject)
    {
        if (_poi == null)
        {
            return;
        }
        
        var colorButton = colorButtonObject as Button;
        Color = colorButton.BackgroundColor;
        _poi.ColorType = Enum.Parse<PoiColorType>(colorButton.ClassId);
        var repository = new Repository();
        await repository.UpdateItemAsync(_poi);
    }

    private async Task DeleteAsync()
    {
        var repository = new Repository();
        await repository.DeleteItemAsync(_poi);
        await BackToHomePage();
    }

    private async Task СoordinatesToClipboardAsync()
        => await Clipboard.Default.SetTextAsync(
            $"{_poi.Latitude.ToString(null, CultureInfo.InvariantCulture)} {_poi.Longitude.ToString(null, CultureInfo.InvariantCulture)}");
}

using AlienScale.Models;
using AlienScale.ViewModels.Commands.LocViewCommands;
using AlienScale.Views.CustomViews;
using AlienScale.Views.LocationBodyView;
using Plugin.Geolocator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AlienScale.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        #region Properties
        private IList _ribbonOptions;
        public IList RibbonOptions
        {
            get
            {
                return _ribbonOptions;
            }
            set
            {
                SetProperty(ref _ribbonOptions, value);
            }
        }
        private IList _locationViewOptions;
        public IList LocationViewOptions
        {
            get { return _locationViewOptions; }
            set
            {
                _locationViewOptions = value;
                SetProperty(ref _locationViewOptions, value);
            }
        }
        public ObservableCollection<CustomPin> CreatedPins { get; set; }

        public Action<Position> OnTapAction { get; set; }
        private ICommand _onTappingMapCommand;
        public ICommand OnTappingMapCommand
        {
            get { return _onTappingMapCommand; }
            set
            {
                _onTappingMapCommand = value;
                OnPropertyChanged("OnTappingMapCommand");
            }
        }
        private ICommand _optionSelectionChangedCommand;
        public ICommand OptionSelectionChangedCommand
        {
            get
            {
                return _optionSelectionChangedCommand;
            }
            set
            {
                SetProperty(ref _optionSelectionChangedCommand, value);
            }
        }
        private RefreshLocCommand _refreshLocCommand;
        public RefreshLocCommand RefreshLocCommand
        {
            get { return _refreshLocCommand; }
            set
            {
                _refreshLocCommand = value;
                OnPropertyChanged("RefreshLocCommand");
            }
        }
        private SaveLocCommand _saveLocCommand;
        public SaveLocCommand SaveLocCommand
        {
            get { return _saveLocCommand; }
            set
            {
                _saveLocCommand = value;
                OnPropertyChanged("SaveLocCommand");
            }
        }
        private DeleteLocCommand _deleteLocCOmmand;
        public DeleteLocCommand DeleteLocCommand
        {
            get { return _deleteLocCOmmand; }
            set
            {
                _deleteLocCOmmand = value;
                OnPropertyChanged("DeleteLocCommand");
            }
        }
        
        private FishingLoc _currentLoc;
        public FishingLoc CurrentLoc
        {
            get { return _currentLoc; }
            set
            {
                _currentLoc = value;
                OnPropertyChanged("CurrentLoc");
            }
        }
        private MapSpan _currentMapSpan;
        public MapSpan CurrentMapSpan
        {
            get { return _currentMapSpan; }
            set
            {
                _currentMapSpan = value;
                OnPropertyChanged("CurrentMapSpan");
            }
        }
        private HomeViewModel _homeViewModel;
        public HomeViewModel HomeViewModel
        {
            get { return _homeViewModel; }
            set
            {
                _homeViewModel = value;
                OnPropertyChanged("HomeViewModel");
            }
        }

        private bool _addLocationIsVisible;
        public bool AddLocationIsVisible
        {
            get { return _addLocationIsVisible; }
            set
            {
                _addLocationIsVisible = value;
                OnPropertyChanged("AddLocationIsVisible");
            }
        }
        private bool _listLocationIsVisible;
        public bool ListLocationIsVisible
        {
            get { return _listLocationIsVisible; }
            set
            {
                _listLocationIsVisible = value;
                OnPropertyChanged("ListLocationIsVisible");
            }
        }
        private bool _statLocationIsVisible;
        public bool StatLocationIsVisible
        {
            get { return _statLocationIsVisible; }
            set
            {
                _statLocationIsVisible = value;
                OnPropertyChanged("StatLocationIsVisible");
            }
        }
        private bool _locIsRefreshing;
        public bool LocIsRefreshing
        {
            get { return _locIsRefreshing; }
            set
            {
                _locIsRefreshing = value;
                OnPropertyChanged("LocIsRefreshing");
            }
        }
        private bool _locActivationSwitchState;
        public bool LocActivationSwitchState
        {
            get { return _locActivationSwitchState; }
            set
            {
                if(value != _locActivationSwitchState)
                {
                    //only on new values
                    _locActivationSwitchState = value;
                    AddLocMapVisible = !AddLocMapVisible;
                    OnPropertyChanged("LocActivationSwitchState");
                }
            }
        }
        private bool _addLocMapVisible;
        public bool AddLocMapVisible
        {
            get { return _addLocMapVisible; }
            set
            {
                _addLocMapVisible = value;
                OnPropertyChanged("AddLocMapVisible");
            }
        }

        private int _selectedLocTab;
        public int SelectedLocTab
        {
            get
            {
                return _selectedLocTab;
            }
            set
            {
                _selectedLocTab = value;
                OnPropertyChanged("SelectedLocTab");
            }
        }
        #endregion
        public LocationViewModel(HomeViewModel viewModel)
        {
            HomeViewModel = viewModel;

            CurrentLoc = new FishingLoc();
            LocActivationSwitchState = true;
            ListLocationIsVisible = true;
            AddLocMapVisible = false;

            CreatedPins = new ObservableCollection<CustomPin>();
            OnTappingMapCommand = new OnTappingMapCommand(this);
            OnTapAction = new Action<Position>(data => AddCreatedPin(data));
            RefreshLocCommand = new RefreshLocCommand(this);
            SaveLocCommand = new SaveLocCommand(this);
            DeleteLocCommand = new DeleteLocCommand(this);
            
            async void getLocation()
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                CurrentMapSpan = new MapSpan(new Position(position.Latitude, position.Longitude), 0.1, 0.1);
            };
            getLocation();

            List<String> lst = new List<String>() {
                    "locations",
                    "stat",
                };
            this.RibbonOptions = lst;

            List<StackLayout> fishOpt = new List<StackLayout>
            {
                new ListLocationBodyView(this),
                new StatLocationBodyView(this)
            };
            this.LocationViewOptions = fishOpt;
            
            this.OptionSelectionChangedCommand = new Command((obj) =>
            {
                var selectedItemRibbonIndex = obj.ToString();
                
                HomeViewModel.IsToolbarMenuOpened = false;
                HomeViewModel.IsMenuSlide = false;
                ListLocationIsVisible = false;
                AddLocationIsVisible = false;
                StatLocationIsVisible = false;

                if (selectedItemRibbonIndex == "0")
                {
                    ListLocationIsVisible = true;
                }
                else if (selectedItemRibbonIndex == "1")
                {
                    StatLocationIsVisible = true;
                }
                else
                {
                    AddLocationIsVisible = true;
                }
            });
        }

        public async Task<bool> InsertLoc(FishingLoc loc, bool fromFish = false)
        {
            if (loc == null)
            {
                loc = new FishingLoc();
            }
            Coordinate coor = new Coordinate() { IdCoordinate = Guid.NewGuid().ToString() };
            if (LocActivationSwitchState)
            {
                //From geolocator
                try
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();
                    if (position != null)
                    {
                        coor.Lattitude = position.Latitude;
                        coor.Longitude = position.Longitude;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //from Selection
                loc.Name = CurrentLoc.Name;
                loc.PlaceCount = CurrentLoc.PlaceCount;
                loc.Type = CurrentLoc.Type;

                if (CreatedPins.Count == 1)
                {
                    foreach (var pin in CreatedPins)
                    {
                        coor.Lattitude = pin.Pin.Position.Latitude;
                        coor.Longitude = pin.Pin.Position.Longitude;
                    }
                }
            }
            Waypoint wp = new Waypoint() { Coordinate = coor, IdWaypoint = Guid.NewGuid().ToString() };
            loc.Waypoints.Add(wp);
            if (fromFish)
            {
                loc.Type = "GPS";
                loc.Name = null;
                loc.PlaceCount = 0;
            }
            else
            {
                loc.Type = CurrentLoc.Type;
                loc.Name = CurrentLoc.Name;
                loc.PlaceCount = CurrentLoc.PlaceCount;
            }

            loc.IdFishingLoc = Guid.NewGuid().ToString();
            loc.User_IdUser = App.user.IdUser;
            FishingLoc.Insert(loc);
            CurrentLoc = new FishingLoc();
            CreatedPins.Clear();
            HomeViewModel.UpdateLocs();
            
            return true;
        }

        public bool DeleteLoc(FishingLoc loc)
        {
            FishingLoc.Delete(loc);
            HomeViewModel.UpdateLocs();
            return true;
        }

        public void AddCreatedPin(Position pos)
        {
            if (pos != null)
            {
                CreatedPins.Clear();

                var pin = new CustomPin();
                pin.Pin = new Pin()
                {
                    Type = Xamarin.Forms.Maps.PinType.SavedPin,
                    Position = new Xamarin.Forms.Maps.Position(pos.Latitude, pos.Longitude),
                    Label = "Created pin"
                };
                CreatedPins.Add(pin);
            }
        }

    }
}

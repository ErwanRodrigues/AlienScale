using AlienScale.Models;
using AlienScale.ViewModels.Commands.FishViewCommands;
using AlienScale.Views.FishBodyView;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.ViewModels
{
    public class FishViewModel : BaseViewModel
    {
        #region Properties
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
        private IList _fishViewOptions;
        public IList FishViewOptions
        {
            get { return _fishViewOptions; }
            set
            {
                _fishViewOptions = value;
                SetProperty(ref _fishViewOptions, value);
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

        private RefreshFishesCommand _refreshFishesCommand;
        public RefreshFishesCommand RefreshFishesCommand
        {
            get
            {
                return _refreshFishesCommand;
            }
            set
            {
                _refreshFishesCommand = value;
                OnPropertyChanged("RefreshFishesCommand");
            }
        }
        private SaveFishCommand _saveFishCommand;
        public SaveFishCommand SaveFishCommand
        {
            get { return _saveFishCommand; }
            set
            {
                _saveFishCommand = value;
                SetProperty(ref _saveFishCommand, value);
            }
        }
        private DeleteFishCommand _deleteFishCommand;
        public DeleteFishCommand DeleteFishCommand
        {
            get { return _deleteFishCommand; }
            set
            {
                _deleteFishCommand = value;
                SetProperty(ref _deleteFishCommand, value);
            }
        }

        private List<FishType> _fishTypes;
        public List<FishType> FishTypes
        {
            get { return _fishTypes; }
            set
            {
                _fishTypes = value;
                OnPropertyChanged("FishTypes");
            }
        }
        private Fish _currentFish;
        public Fish CurrentFish
        {
            get { return _currentFish; }
            set
            {
                _currentFish = value;
                OnPropertyChanged("CurrentFish");
            }
        }
        private FishType _currentFishType;
        public FishType CurrentFishType
        {
            get { return _currentFishType; }
            set
            {
                _currentFishType = value;
                OnPropertyChanged("CurrentFishType");
            }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                CurrentFish = new Fish()
                {
                    Weight = this.Weight,
                    Length = this.Length,
                    Bait = this.Bait,
                    FishPictureUrl = this.FishPictureUrl,
                    FishType = CurrentFishType,
                };
                OnPropertyChanged("Weight");
            }
        }
        private string _weightstr;
        public string Weightstr
        {
            get { return _weightstr; }
            set
            {
                _weightstr = value;
                if (!string.IsNullOrEmpty(_weightstr))
                    this.Weight = double.Parse(value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                else
                    this.Weight = 0;
                OnPropertyChanged("Weightstr");
            }
        }
        private double _length;
        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                CurrentFish = new Fish()
                {
                    Length = this.Length,
                    Weight = this.Weight,
                    Bait = this.Bait,
                    FishPictureUrl = this.FishPictureUrl,
                    FishType = CurrentFishType,
                };
                OnPropertyChanged("Length");
            }
        }
        private string _lengthstr;
        public string Lengthstr
        {
            get { return _lengthstr; }
            set
            {
                _lengthstr = value;
                if (!string.IsNullOrEmpty(_lengthstr))
                    this.Length = double.Parse(value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                else
                    this.Length = 0;
                OnPropertyChanged("Lengthstr");
            }
        }
        private string _fishPictureUrl;
        public string FishPictureUrl
        {
            get { return _fishPictureUrl; }
            set
            {
                _fishPictureUrl = value;
                CurrentFish = new Fish()
                {
                    FishPictureUrl = this.FishPictureUrl,
                    Weight = this.Weight,
                    Bait = this.Bait,
                    Length = this.Length,
                    FishType = CurrentFishType,
                };
                OnPropertyChanged("FishPictureUrl");
            }
        }
        private string _bait;
        public string Bait
        {
            get { return _bait; }
            set
            {
                _bait = value;
                CurrentFish = new Fish()
                {
                    Bait = this.Bait,
                    Weight = this.Weight,
                    Length = this.Length,
                    FishPictureUrl = this.FishPictureUrl,
                    FishType = CurrentFishType,
                };
                OnPropertyChanged("Bait");
            }
        }

        private int _selectedFishTab;
        public int SelectedFishTab
        {
            get
            {
                return _selectedFishTab;
            }
            set
            {
                _selectedFishTab = value;
                OnPropertyChanged("SelectedFishTab");
            }
        }

        private object _locPickerItemSelected;
        public object LocPickerItemSelected
        {
            get { return _locPickerItemSelected; }
            set
            {
                _locPickerItemSelected = value;
                OnPropertyChanged("LocPickerItemSelected");
            }
        }
        private object _typePickerItemSelected;
        public object TypePickerItemSelected
        {
            get { return _typePickerItemSelected; }
            set
            {
                _typePickerItemSelected = value;
                if (_typePickerItemSelected != null)
                    if (CurrentFish != null)
                        CurrentFishType = (FishType)_typePickerItemSelected;

                OnPropertyChanged("TypePickerItemSelected");
            }
        }

        private bool _addFishIsVisible;
        public bool AddFishIsVisible
        {
            get { return _addFishIsVisible; }
            set
            {
                _addFishIsVisible = value;
                OnPropertyChanged("AddFishIsVisible");
            }
        }
        private bool _listFishIsVisible;
        public bool ListFishIsVisible
        {
            get { return _listFishIsVisible; }
            set
            {
                _listFishIsVisible = value;
                OnPropertyChanged("ListFishIsVisible");
            }
        }
        private bool _statFishIsVisible;
        public bool StatFishIsVisible
        {
            get { return _statFishIsVisible; }
            set
            {
                _statFishIsVisible = value;
                OnPropertyChanged("StatFishIsVisible");
            }
        }
        private bool _locationOptionVisible;
        public bool LocationOptionVisible
        {
            get { return _locationOptionVisible; }
            set
            {
                _locationOptionVisible = value;
                OnPropertyChanged("LocationOptionVisible");
            }
        }
        private bool _autoLocationSwitchState;
        public bool AutoLocationSwitchState
        {
            get { return _autoLocationSwitchState; }
            set
            {
                //only on new values
                if(value != _autoLocationSwitchState)
                {
                    _autoLocationSwitchState = value;
                    LocationOptionVisible = !LocationOptionVisible;
                    OnPropertyChanged("AutoLocationSwitchState");
                }
            }
        }
        private bool _manuLocationSwitchState;
        public bool ManuLocationSwitchState
        {
            get { return _manuLocationSwitchState; }
            set
            {
                _manuLocationSwitchState = value;
                OnPropertyChanged("ManuLocationSwitchState");
            }
        }
        private bool _fishIsRefreshing;
        public bool FishIsRefreshing
        {
            get { return _fishIsRefreshing; }
            set
            {
                _fishIsRefreshing = value;
                OnPropertyChanged("FishIsRefreshing");
            }
        }
        private bool _outOfSessionFieldsVisible;
        public bool OutOfSessionFieldsVisible
        {
            get { return _outOfSessionFieldsVisible; }
            set
            {
                _outOfSessionFieldsVisible = value;
                OnPropertyChanged("OutOfSessionFieldsVisible");
            }
        }
        #endregion

        public FishViewModel(HomeViewModel homeViewModel)
        {
            HomeViewModel = homeViewModel;

            // init members
            ListFishIsVisible = true;
            AddFishIsVisible = false;
            StatFishIsVisible = false;
            AutoLocationSwitchState = true;
            LocationOptionVisible = false;
            ManuLocationSwitchState = true;

            CurrentFish = new Fish();
            FishTypes = new List<FishType>(); UpdateFishTypes();
            RefreshFishesCommand = new RefreshFishesCommand(this);
            SaveFishCommand = new SaveFishCommand(this);
            DeleteFishCommand = new DeleteFishCommand(this);

            List<String> lst = new List<String>() {
                    "fish",
                    "stat",
                };
            this.RibbonOptions = lst;

            List<StackLayout> fishOpt = new List<StackLayout>
            {
                new ListFishBodyView(this),
                new StatFishBodyView(this)
            };
            this.FishViewOptions = fishOpt;

            this.OptionSelectionChangedCommand = new Command((obj) =>
            {
                var selectedItemRibbonIndex = obj.ToString();

                HomeViewModel.IsToolbarMenuOpened = false;
                ListFishIsVisible = false;
                AddFishIsVisible = false;
                StatFishIsVisible = false;

                if (selectedItemRibbonIndex == "0")
                {
                    ListFishIsVisible = true;
                }
                else if (selectedItemRibbonIndex == "1")
                {
                    StatFishIsVisible = true;
                }
                else
                {
                    CurrentFish = new Fish();
                    TypePickerItemSelected = null;
                    AddFishIsVisible = true;
                }
            });
        }

        public bool UpdateFishTypes()
        {
            try
            {
                var types = FishType.GetFishTypes();

                if (types != null)
                {
                    FishTypes.Clear();
                    foreach (var type in types)
                        FishTypes.Add(type);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InsertFish(Fish fish)
        {
            if (fish == null)
                fish = new Fish();
            if (fish.Weight > 0 && TypePickerItemSelected != null)
            {
                if (!AutoLocationSwitchState)
                {
                    var locator = CrossGeolocator.Current;
                    var position = await locator.GetPositionAsync();
                    if (position != null)
                    {
                        //we need to add this new auto generated location to DB 
                        fish.FishingLoc = new FishingLoc()
                        {
                            IdFishingLoc = Guid.NewGuid().ToString(),
                            Type = "GPS",
                            Name = null,
                            PlaceCount = 0
                        };
                        fish.FishingLoc.Waypoints.Add(
                            new Waypoint
                            {
                                IdWaypoint = Guid.NewGuid().ToString(),
                                Coordinate = new Coordinate
                                {
                                    IdCoordinate = Guid.NewGuid().ToString(),
                                    Lattitude = position.Latitude,
                                    Longitude = position.Longitude
                                }
                            });
                        FishingLoc.Insert(fish.FishingLoc);
                    }
                }
                else
                {
                    if (LocPickerItemSelected != null && ManuLocationSwitchState)
                    {
                        fish.FishingLoc = (FishingLoc)LocPickerItemSelected;
                    }
                    else if (!ManuLocationSwitchState)
                    {
                        fish.FishingLoc = null;
                    }
                }

                if (fish.FishingLoc != null)
                {
                    //When inserted from this method, Locations should have only 1 waypoint so we take this one in reference
                    fish.Waypoint_IdWaypoint = ((from w in fish.FishingLoc.Waypoints select w).ToList().FirstOrDefault()).IdWaypoint;
                }

                //link the fish with the fish type
                fish.FishType = ((FishType)TypePickerItemSelected);
                fish.CatchedDateTime = DateTime.Now;
                fish.User_IdUser = App.user.IdUser;
                fish.Bait = CurrentFish.Bait;
                fish.Weight = CurrentFish.Weight;
                fish.Length = CurrentFish.Length;
                fish.IdFish = Guid.NewGuid().ToString();
                Fish.Insert(fish);
                if (CurrentFish != null)
                {
                    //ResetingAddFields();
                    HomeViewModel.UpdateFishes();
                    return true;
                }
                return false;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Empty field", "Please fill the type and the weight", "OK");
                return false;
            }
        }

        public async Task<bool> InsertFishInSession(Fish fish)
        {
            if (fish.Weight > 0 && TypePickerItemSelected != null)
            {
                if (HomeViewModel.RunningSession.FishingLoc != null)
                {
                    if (HomeViewModel.RunningSession.FishingLoc.Name != "GPS")
                    {
                        //known location
                        CurrentFish.FishingLoc_IdFishingLoc = HomeViewModel.RunningSession.FishingLoc_IdFishingLoc;
                    }
                    else
                    {
                        //get current Loc
                        var locator = CrossGeolocator.Current;
                        var position = await locator.GetPositionAsync();
                        if (position != null)
                        {
                            //add the waypoint to the fishing Loc in the session & in the Fish
                            Waypoint wp = new Waypoint
                            {
                                IdWaypoint = Guid.NewGuid().ToString(),
                                Coordinate = new Coordinate
                                {
                                    IdCoordinate = Guid.NewGuid().ToString(),
                                    Lattitude = position.Latitude,
                                    Longitude = position.Longitude
                                }
                            };
                            HomeViewModel.RunningSession.FishingLoc.Waypoints.Add(wp);
                            CurrentFish.Waypoint_IdWaypoint = wp.IdWaypoint;
                        }
                    }
                }
                //link the fish with the fish type
                fish.FishType = ((FishType)TypePickerItemSelected);
                fish.CatchedDateTime = DateTime.Now;
                fish.User_IdUser = App.user.IdUser;
                fish.Bait = CurrentFish.Bait;
                fish.Weight = CurrentFish.Weight;
                fish.Length = CurrentFish.Length;
                fish.IdFish = Guid.NewGuid().ToString();
                fish.Session_IdSession = HomeViewModel.RunningSession.IdSession;
                HomeViewModel.RunningSession.Fishes.Add(fish);
                Session.InsertFullSession(HomeViewModel.RunningSession);
                //ResetingAddFields();
                HomeViewModel.UpdateFishes();
                HomeViewModel.UpdateSessions();
            }
            return true;
        }

        public void ResetingAddFields()
        {
            //reseting fields
            Weightstr = string.Empty;
            Weight = 0;
            Lengthstr = string.Empty;
            Length = 0;
            Bait = string.Empty;
            TypePickerItemSelected = null;
        }
    }
}

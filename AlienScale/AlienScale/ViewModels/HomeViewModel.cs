using AlienScale.Models;
using AlienScale.Views.CustomViews;
using AlienScale.Views.Popups;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace AlienScale.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
        //collections & context
        public ObservableCollection<Fish> Fishes { get; set; }
        public ObservableCollection<FishingLoc> Locs { get; set; }
        public ObservableCollection<Session> Sessions { get; set; }
        public ObservableCollection<CustomPin> Pins { get; set; }
        private Session _runningSession;
        public Session RunningSession
        {
            get { return _runningSession; }
            set
            {
                _runningSession = value;
                OnPropertyChanged("RunningSession");
            }
        }

        //children view models
        private FishViewModel _fishViewModel;
        public FishViewModel FishViewModel
        {
            get { return _fishViewModel; }
            set
            {
                _fishViewModel = value;
                OnPropertyChanged("FishViewModel");
            }
        }
        private LocationViewModel _locationViewModel;
        public LocationViewModel LocationViewModel
        {
            get { return _locationViewModel; }
            set
            {
                _locationViewModel = value;
                OnPropertyChanged("LocationViewModel");
            }
        }
        private SessionViewModel _sessionViewModel;
        public SessionViewModel SessionViewModel
        {
            get { return _sessionViewModel; }
            set
            {
                _sessionViewModel = value;
                OnPropertyChanged("SessionViewModel");
            }
        }
        private MapViewModel _mapViewModel;
        public MapViewModel MapViewModel
        {
            get { return _mapViewModel; }
            set
            {
                _mapViewModel = value;
                OnPropertyChanged("MapViewModel");
            }
        }

        //toolbar properties
        private bool _runningSessionIsVisible;
        public bool RunningSessionIsVisible
        {
            get { return _runningSessionIsVisible; }
            set
            {
                _runningSessionIsVisible = value;
                OnPropertyChanged("RunningSessionIsVisible");
            }
        }
        private int _compressLayoutLevel;
        public int CompressLayoutLevel
        {
            get { return _compressLayoutLevel; }
            set
            {
                _compressLayoutLevel = value;
                OnPropertyChanged("CompressLayoutLevel");
            }
        }
        private bool _isToolbarMenuOpened;
        public bool IsToolbarMenuOpened
        {
            get { return _isToolbarMenuOpened; }
            set
            {
                _isToolbarMenuOpened = value;
                if (_isToolbarMenuOpened)
                    //special management for the map view
                    if (MapIsVisible)
                        this.CompressLayoutLevel = 3;
                    else
                        this.CompressLayoutLevel = 2;
                else if (this.RunningSessionIsVisible == true)
                    this.CompressLayoutLevel = 1;
                else
                    this.CompressLayoutLevel = 0;
                OnPropertyChanged("IsToolbarMenuOpened");
            }
        }
        private ICommand _toolbarAddFishCommand;
        public ICommand ToolbarAddFishCommand
        {
            get { return _toolbarAddFishCommand; }
            set
            {
                _toolbarAddFishCommand = value;
                OnPropertyChanged("ToolAddFishCommand");
            }
        }
        private ICommand _toolbarViewSessionCommand;
        public ICommand ToolbarViewSessionCommand
        {
            get { return _toolbarViewSessionCommand; }
            set
            {
                _toolbarViewSessionCommand = value;
                OnPropertyChanged("ToolAddFishCommand");
            }
        }
        private ICommand _toolbarStopSessionCommand;
        public ICommand ToolbarStopSessionCommand
        {
            get { return _toolbarStopSessionCommand; }
            set
            {
                _toolbarStopSessionCommand = value;
                OnPropertyChanged("ToolbarStopSessionCommand");
            }
        }
        private ICommand _displayAddPopupCommand;
        public ICommand DisplayAddPopupCommand
        {
            get { return _displayAddPopupCommand; }
            set
            {
                _displayAddPopupCommand = value;
                OnPropertyChanged("DisplayAddPopup");
            }
        }

        //activty indicator
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        //tabbed page items
        private bool _fishIsVisible;
        public bool FishIsVisible
        {
            get { return _fishIsVisible; }
            set
            {
                _fishIsVisible = value;
                OnPropertyChanged("FishIsVisible");
            }
        }
        private bool _sessionIsVisible;
        public bool SessionIsVisible
        {
            get { return _sessionIsVisible; }
            set
            {
                _sessionIsVisible = value;
                OnPropertyChanged("SessionIsVisible");
            }
        }
        private bool _locationIsVisible;
        public bool LocationIsVisible
        {
            get { return _locationIsVisible; }
            set
            {
                _locationIsVisible = value;
                OnPropertyChanged("LocationIsVisible");
            }
        }
        private bool _mapIsVisible;
        public bool MapIsVisible
        {
            get { return _mapIsVisible; }
            set
            {
                _mapIsVisible = value;
                OnPropertyChanged("MapIsVisible");
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

        //Slide Menu
        private bool _isMenuSlide;
        public bool IsMenuSlide
        {
            get { return _isMenuSlide; }
            set
            {
                _isMenuSlide = value;
                OnPropertyChanged("IsMenuSlide");
            }
        }
        private ICommand _outOfmenuTappedCommand;
        public ICommand OutOfMenuTappedCommand
        {
            get { return _outOfmenuTappedCommand; }
            set
            {
                _outOfmenuTappedCommand = value;
                OnPropertyChanged("OutOfMenuTappedCommand");
            }
        }
        private ICommand _openSlideMenu;
        public ICommand OpenSlideMenu
        {
            get { return _openSlideMenu; }
            set
            {
                _openSlideMenu = value;
                OnPropertyChanged("OpenSlideMenu");
            }
        }
        private double _menuDefaultWidth;
        public double MenuDefaultWidth
        {
            get { return _menuDefaultWidth; }
            set
            {
                _menuDefaultWidth = value;
                OnPropertyChanged("MenuDefaultWidth");
            }
        }
        private IList _menuItems;
        public IList MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                OnPropertyChanged("MenuItems");
            }
        }
        private MenuItem _menuItemSelected;
        public MenuItem MenuItemSelected
        {
            get { return _menuItemSelected; }
            set
            {
                _menuItemSelected = value;
                OnPropertyChanged("MenuItemSelected");
            }
        }
        private ICommand _menuItemSelectedCommand;
        public ICommand MenuItemSelectedCommand
        {
            get { return _menuItemSelectedCommand; }
            set
            {
                _menuItemSelectedCommand = value;
                OnPropertyChanged("MenuItemSelectedCommand");
            }
        }

        //Home page properties
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
        private ICommand _floatingButtonCommand;
        public ICommand FloatingButtonCommand
        {
            get { return _floatingButtonCommand; }
            set
            {
                _floatingButtonCommand = value;
                OnPropertyChanged("FloatingButtonCommand");
            }
        }
        private bool _isAddButtonVisible;
        public bool IsAddButtonVisible
        {
            get { return _isAddButtonVisible; }
            set
            {
                _isAddButtonVisible = value;
                OnPropertyChanged("IsAddButtonVisible");
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                SetProperty(ref _title, value);
            }
        }
        private int _selectedTab;
        public int SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                SetProperty(ref _selectedTab, value);
            }
        }

        #endregion

        public HomeViewModel()
        {
            RunningSession = new Session();
            RunningSessionIsVisible = false;
            IsAddButtonVisible = true;

            OpenSlideMenu = new Command(OpenMenu);
            OutOfMenuTappedCommand = new Command(CloseMenu);
            MenuDefaultWidth = Device.Info.ScaledScreenSize.Width;
            IsMenuSlide = false;

            Fishes = new ObservableCollection<Fish>();
            UpdateFishes();

            Locs = new ObservableCollection<FishingLoc>();
            UpdateLocs();

            Sessions = new ObservableCollection<Session>();
            UpdateSessions();

            Pins = new ObservableCollection<CustomPin>();
            UpdatePins();

            FishViewModel = new FishViewModel(this);
            LocationViewModel = new LocationViewModel(this);
            SessionViewModel = new SessionViewModel(this);
            MapViewModel = new MapViewModel(this);

            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem() { ItemName = "Bluetooth", MenuPage = new BluetoothParameterPopup() {BindingContext = this } });
            MenuItems.Add(new MenuItem() { ItemName = "Themes", MenuPage = new ThemesParameterPopup() { BindingContext = this } });

            MenuItemSelectedCommand = new Command(async (obj) =>
            {
                CloseMenu();
                await PopupNavigation.Instance.PushAsync(MenuItemSelected.MenuPage);
                while (PopupNavigation.Instance.PopupStack.ToList().Count() > 0)
                {
                    await Task.Delay(50);
                }
            });

            List<String> lst = new List<String>() {
                    "fish",
                    "sessions",
                    "locations",
                    "map",
            };
            this.RibbonOptions = lst;
            this.ToolbarViewSessionCommand = new Command(async (obj) =>
            {
                await PopupNavigation.Instance.PushAsync(new ViewSessionPopup() { BindingContext = this });
                while (PopupNavigation.Instance.PopupStack.ToList().Count() > 0)
                {
                    await Task.Delay(50);
                }
                IsToolbarMenuOpened = false;
            });
            this.ToolbarAddFishCommand = new Command(async (obj) =>
            {
                //Clean CurrentFish
                FishViewModel.ResetingAddFields();

                await PopupNavigation.Instance.PushAsync(new AddFishPopup(FishViewModel));
                while (PopupNavigation.Instance.PopupStack.ToList().Count() > 0)
                {
                    await Task.Delay(50);
                }

            });
            this.ToolbarStopSessionCommand = new Command(async (obj) =>
            {
                await AskStoppingSession();
            });
            this.DisplayAddPopupCommand = new Command(async (obj) =>
            {
                if (FishIsVisible)
                {
                    FishViewModel.ResetingAddFields();
                    if (FishViewModel.HomeViewModel.RunningSession.IsRunning == true)
                    {
                        this.FishViewModel.OutOfSessionFieldsVisible = false;
                        this.ToolbarAddFishCommand.Execute(null);
                        //Refresh current session
                        RunningSession = Session.GetFullSessionById(RunningSession.IdSession);
                        IsToolbarMenuOpened = false;
                    }
                    else
                    {
                        this.FishViewModel.OutOfSessionFieldsVisible = true;
                        this.ToolbarAddFishCommand.Execute(null);
                    }
                }
                else if (SessionIsVisible)
                {
                    await PopupNavigation.Instance.PushAsync(new AddSessionPopup(SessionViewModel));
                    while (PopupNavigation.Instance.PopupStack.ToList().Count() > 0)
                    {
                        await Task.Delay(50);
                    }
                }
                else if (LocationIsVisible)
                {
                    await PopupNavigation.Instance.PushAsync(new AddLocationPopup(LocationViewModel));
                    while (PopupNavigation.Instance.PopupStack.ToList().Count() > 0)
                    {
                        await Task.Delay(50);
                    }
                }
            });

            GetRunningSession();

            IsBusy = true;
            FishIsVisible = true;

            this.OptionSelectionChangedCommand = new Command((obj) => {
                var selectedItemRibbonIndex = obj.ToString();

                IsToolbarMenuOpened = false;
                CloseMenu();
                FishIsVisible = false;
                SessionIsVisible = false;
                LocationIsVisible = false;
                MapIsVisible = false;

                IsAddButtonVisible = true;

                if (selectedItemRibbonIndex == "0")
                {
                    Title = "Fish";
                    FishIsVisible = true;
                }
                else if (selectedItemRibbonIndex == "1")
                {
                    Title = "Session";
                    SessionIsVisible = true;

                }
                else if (selectedItemRibbonIndex == "2")
                {
                    Title = "Location";
                    LocationIsVisible = true;
                }
                else
                {
                    Title = "Map";
                    IsAddButtonVisible = false;
                    MapIsVisible = true;
                }
            });
        }

        public void CloseMenu()
        {
            IsMenuSlide = false;
        }

        public void OpenMenu()
        {
            if (IsMenuSlide)
            {
                IsMenuSlide = false;
            }
            else
            {
                IsMenuSlide = true;
            }
        }

        public async Task<bool> AskStoppingSession()
        {
            var ans = await App.Current.MainPage.DisplayAlert("Running session", string.Format("Session {0} is running, Would you like to stop this session?", RunningSession.Name), "Yes", "No");
            if (ans)
            {
                //make sessionButton visible
                this.RunningSession.IsRunning = false;
                Session.InsertFullSession(RunningSession);
                this.RunningSessionIsVisible = false;
                this.CompressLayoutLevel = 0;
            }
            else
            {
                this.IsToolbarMenuOpened = false;
            }
            return true;
        }

        public bool UpdateFishes()
        {
            try
            {
                var fishes = Fish.GetFishesByUserId(App.user.IdUser);

                if (fishes != null)
                {
                    Fishes.Clear();
                    foreach (var fish in fishes)
                        Fishes.Add(fish);
                }
                UpdatePins();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLocs()
        {
            try
            {
                var locs = FishingLoc.GetNamedFishingLoc(App.user.IdUser);

                if (locs != null)
                {
                    Locs.Clear();
                    foreach (var loc in locs)
                        Locs.Add(loc);
                }
                UpdatePins();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSessions()
        {
            try
            {
                var sess = Session.GetSessions(App.user.IdUser);

                if (sess != null)
                {
                    Sessions.Clear();
                    foreach (var session in sess)
                    {
                        Sessions.Add(session);
                    }
                    //GetRunningSession();
                }
                UpdatePins();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePins()
        {
            try
            {
                Pins.Clear();
                if (Sessions != null)
                {
                    foreach (var session in Sessions)
                    {
                        //get only sessions where Waypoints =! null
                        if (session.FishingLoc.Waypoints != null)
                        {
                            if (session.FishingLoc.Waypoints.Count > 0)
                            {
                                //get first waypoint
                                Waypoint wp = (from p in session.FishingLoc.Waypoints
                                               orderby p.Order
                                               select p).FirstOrDefault();

                                if (wp.Coordinate != null)
                                {
                                    var pin = new CustomPin();
                                    pin.Pin = new Pin()
                                    {
                                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                                        Position = new Position(wp.Coordinate.Lattitude, wp.Coordinate.Longitude),
                                        Label = string.Format("{0};{1};{2};{3};{4};{5};{6}", "session", session.Name, string.Format("{0} fish(es)", session.TotalCatches), string.Format("{0:0.000} Kg", session.TotalWeight), session.IsRunning.ToString(), string.Format("Starts : {0:dd/MM/yyyy hh:mm}", session.Starts), string.Format("Ends : {0:dd/MM/yyyy hh:mm}", session.Ends))
                                    };
                                    foreach (var w in session.FishingLoc.Waypoints)
                                    {
                                        if (w != null)
                                        {
                                            Position pos = new Position(w.Coordinate.Lattitude, w.Coordinate.Longitude);
                                            pin.RouteCoordinates.Add(pos);
                                        }
                                    }
                                    Pins.Add(pin);
                                }
                            }
                        }
                    }
                }

                if (Fishes != null)
                {
                    foreach (var fish in Fishes)
                    {
                        //select only fishes out of sessions & out of named loc for pins
                        if (fish.Session_IdSession == null || fish.Session_IdSession == string.Empty)
                        {
                            if (fish.FishingLoc.Name != null || fish.FishingLoc.Name != string.Empty)
                            {
                                if (fish.Waypoint_IdWaypoint != null)
                                {
                                    //get the waypoint 
                                    var wp = (from p in fish.FishingLoc.Waypoints
                                              where p.IdWaypoint == fish.Waypoint_IdWaypoint
                                              select p).FirstOrDefault();

                                    var pin = new CustomPin();
                                    pin.Pin = new Pin()
                                    {
                                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                                        Position = new Xamarin.Forms.Maps.Position(wp.Coordinate.Lattitude, wp.Coordinate.Longitude),
                                        Label = string.Format("{0};{1};{2};{3};{4}", "fish", fish.FishType.TypeName, string.Format("{0:0.000} kg", fish.Weight), string.Format("{0:0.000} cm", fish.Length), string.Format("Catched on : {0:dd/MM/yyyy hh:mm}", fish.CatchedDateTime))
                                    };

                                    Pins.Add(pin);
                                }
                            }
                        }
                    }
                }

                if (Locs != null)
                {
                    foreach (var spot in Locs)
                    {
                        if (!(spot.Waypoints.Count > 1))
                        {
                            foreach (var wp in spot.Waypoints)
                            {
                                if (wp.Coordinate != null)
                                {
                                    var pin = new CustomPin();
                                    pin.Pin = new Pin()
                                    {
                                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                                        Position = new Xamarin.Forms.Maps.Position(wp.Coordinate.Lattitude, wp.Coordinate.Longitude),
                                        Label = string.Format("{0};{1};{2};{3}", "location", spot.Name, string.Format("{0} fish(es)", spot.TotalCatches), string.Format("{0:0.000} Kg", spot.TotalWeight)),

                                    };
                                    Pins.Add(pin);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async void GetRunningSession()
        {
            foreach (var session in Sessions)
            {
                if (session.IdSession != RunningSession.IdSession)
                {
                    if (session.IsRunning == true)
                    {
                        //running session found
                        RunningSession = session;
                        var ans = await App.Current.MainPage.DisplayAlert("Running session", string.Format("Session {0} is already running, Would you like to continue?", RunningSession.Name), "Yes", "No");
                        if (ans)
                        {
                            //If yes then Stop the Get session and display toolbar
                            RunningSessionIsVisible = true;
                            this.CompressLayoutLevel = 1;
                            break;
                        }
                        else
                        {
                            RunningSession.IsRunning = false;
                            Session.InsertFullSession(RunningSession);
                        }
                    }
                }
            }
        }

        public void StartSession()
        {
            //make sessionButton visible
            RunningSessionIsVisible = true;
            this.CompressLayoutLevel = 1;
        }
    }

    public class MenuItem
    {
        public string ItemName { get; set; }
        public PopupPage MenuPage { get; set; }
    }
}

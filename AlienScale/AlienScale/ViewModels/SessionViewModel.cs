using AlienScale.Models;
using AlienScale.ViewModels.Commands.SessionViewCommands;
using AlienScale.Views;
using AlienScale.Views.SessionBodyView;
using Plugin.Geolocator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.ViewModels
{
    public class SessionViewModel : BaseViewModel
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
        private IList _SessionViewOptions;
        public IList SessionViewOptions
        {
            get { return _SessionViewOptions; }
            set
            {
                _SessionViewOptions = value;
                SetProperty(ref _SessionViewOptions, value);
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
        private SaveSessionCommand _saveSessionCommand;
        public SaveSessionCommand SaveSessionCommand
        {
            get { return _saveSessionCommand; }
            set
            {
                _saveSessionCommand = value;
                OnPropertyChanged("SaveSessionCommand");
            }
        }
        private RefreshSessionCommand _refreshSessionCommand;
        public RefreshSessionCommand RefreshSessionCommand
        {
            get { return _refreshSessionCommand; }
            set
            {
                _refreshSessionCommand = value;
                OnPropertyChanged("RefreshSessionCommand");
            }
        }
        private DeleteSessionCommand _deleteSessionCommand;
        public DeleteSessionCommand DeleteSessionCommand
        {
            get { return _deleteSessionCommand; }
            set
            {
                _deleteSessionCommand = value;
                OnPropertyChanged("DeleteSessionCommand");
            }
        }

        private Session _currentSession;
        public Session CurrentSession
        {
            get { return _currentSession; }
            set
            {
                _currentSession = value;
                OnPropertyChanged("CurrentSession");
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
        
        private int _selectedSessionTab;
        public int SelectedSessionTab
        {
            get
            {
                return _selectedSessionTab;
            }
            set
            {
                SetProperty(ref _selectedSessionTab, value);
            }
        }
        private object _sessionLocPickerSelectedItem;
        public object SessionLocPickerSelectedItem
        {
            get { return _sessionLocPickerSelectedItem; }
            set
            {
                _sessionLocPickerSelectedItem = value;
                OnPropertyChanged("SessionLocPickerSelectedItem");
            }
        }

        private bool _addSessionIsVisible;
        public bool AddSessionIsVisible
        {
            get { return _addSessionIsVisible; }
            set
            {
                _addSessionIsVisible = value;
                OnPropertyChanged("AddSessionIsVisible");
            }
        }
        private bool _listSessionIsVisible;
        public bool ListSessionIsVisible
        {
            get { return _listSessionIsVisible; }
            set
            {
                _listSessionIsVisible = value;
                OnPropertyChanged("ListSessionIsVisible");
            }
        }
        private bool _statSessionIsVisible;
        public bool StatSessionIsVisible
        {
            get { return _statSessionIsVisible; }
            set
            {
                _statSessionIsVisible = value;
                OnPropertyChanged("StatSessionIsVisible");
            }
        }
        private bool _timeParamSessionIsVisible;
        public bool TimeParamSessionIsVisible
        {
            get { return _timeParamSessionIsVisible; }
            set
            {
                _timeParamSessionIsVisible = value;
                OnPropertyChanged("TimeParamSessionIsVisible");
            }
        }
        private bool _locParamSessionIsVisible;
        public bool LocParamSessionIsVisible
        {
            get { return _locParamSessionIsVisible; }
            set
            {
                _locParamSessionIsVisible = value;
                OnPropertyChanged("LocParamSessionIsVisible");
            }
        }
        private bool _sessionLocationPickerVisible;
        public bool SessionLocationPickerVisible
        {
            get { return _sessionLocationPickerVisible; }
            set
            {
                _sessionLocationPickerVisible = value;
                OnPropertyChanged("SessionLocationPickerVisible");
            }
        }
        private bool _timeParamSessionSwitchState;
        public bool TimeParamSessionSwitchState
        {
            get { return _timeParamSessionSwitchState; }
            set
            {
                _timeParamSessionSwitchState = value;
                TimeParamSessionIsVisible = _timeParamSessionSwitchState;
                OnPropertyChanged("TimeParamSessionSwitchState");
            }
        }
        private bool _locParamSessionSwitchState;
        public bool LocParamSessionSwitchState
        {
            get { return _locParamSessionSwitchState; }
            set
            {
                _locParamSessionSwitchState = value;
                LocParamSessionIsVisible = _locParamSessionSwitchState;
                OnPropertyChanged("LocParamSessionSwitchState");
            }
        }
        private bool _locTrackingSessionSwitchState;
        public bool LocTrackingSessionSwitchState
        {
            get { return _locTrackingSessionSwitchState; }
            set
            {
                _locTrackingSessionSwitchState = value;
                SessionLocationPickerVisible = !_locTrackingSessionSwitchState;
                OnPropertyChanged("LocTrackingSessionSwitchState");
            }
        }
        private bool _sessionIsRefreshing;
        public bool SessionIsRefreshing
        {
            get { return _sessionIsRefreshing; }
            set
            {
                _sessionIsRefreshing = value;
                OnPropertyChanged("SessionIsRefreshing");
            }
        }
        #endregion
        public SessionViewModel(HomeViewModel viewModel)
        {
            HomeViewModel = viewModel;

            ListSessionIsVisible = true;
            TimeParamSessionSwitchState = false;
            LocParamSessionSwitchState = false;
            LocTrackingSessionSwitchState = true;
            CurrentSession = new Session();

            SaveSessionCommand = new SaveSessionCommand(this);
            RefreshSessionCommand = new RefreshSessionCommand(this);
            DeleteSessionCommand = new DeleteSessionCommand(this);

            List<String> lst = new List<String>() {
                    "sessions",
                    "stat",
                };
            this.RibbonOptions = lst;

            List<StackLayout> SessionOpt = new List<StackLayout>
            {
                new ListSessionBodyView(this),
                new StatSessionBodyView(this)
            };
            this.SessionViewOptions = SessionOpt;
            
            this.OptionSelectionChangedCommand = new Command((obj) => {
                var selectedItemRibbonIndex = obj.ToString();

               
                HomeViewModel.IsToolbarMenuOpened = false;

                ListSessionIsVisible = false;
                AddSessionIsVisible = false;
                StatSessionIsVisible = false;

                if (selectedItemRibbonIndex == "0")
                {
                    ListSessionIsVisible = true;
                }
                
                else if (selectedItemRibbonIndex == "1")
                {
                    StatSessionIsVisible = true;
                }
                else
                {
                    AddSessionIsVisible = true;
                }
            });
        }

        public async Task<bool> DeleteSession(Session session)
        {
            if(session.IsRunning == true)
            {
                //stop the session
               await _homeViewModel.AskStoppingSession();
            }
            session.FishingLoc = null;
            if (session.Fishes != null )
            {
                if (session.Fishes.Count > 0)
                {
                    var ans2 = await App.Current.MainPage.DisplayAlert("Cascade deletion", "You are trying to delete a session with fishes, \nDou you want to delete all the fishes?", "Yes", "No");
                    if (ans2)
                    {
                        Session.Delete(session, true);
                        return true;
                    }
                    else
                    {
                        var answer = await App.Current.MainPage.DisplayAlert("Confirmation", "Do you still want to delete the session?", "Yes", "No");
                        if (answer)
                        {
                            Session.Delete(session);
                            return true;
                        } 
                    }
                }
                else
                    Session.Delete(session);
            }
            else
                Session.Delete(session);
            return true;
        }

        public async Task<bool> InsertSession(Session session)
        {
            //Checking which location param has been selected
            if (!LocParamSessionSwitchState || (LocParamSessionSwitchState && LocTrackingSessionSwitchState))
            {
                //starting getting current location
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                //create coordinates
                Coordinate coor = new Coordinate() { IdCoordinate = Guid.NewGuid().ToString(), Lattitude = position.Latitude, Longitude = position.Longitude };
                Waypoint wp = new Waypoint() { IdWaypoint = Guid.NewGuid().ToString(), Coordinate = coor, };
                session.FishingLoc = new FishingLoc() { IdFishingLoc = Guid.NewGuid().ToString(), Name="GPS", Waypoints = new List<Waypoint>() { wp } };
            }
            else
            {
                session.FishingLoc = (FishingLoc)SessionLocPickerSelectedItem;
            }

            //Checking which time param has been selected
            if (TimeParamSessionSwitchState)
            {
                session.Starts = CurrentSession.Starts;
                session.Ends = CurrentSession.Ends;
                session.IsRunning = false;
            }
            else
            {
                session.Starts = DateTime.Now;
                session.IsRunning = true;

            }
            session.IdSession = Guid.NewGuid().ToString();
            session.User_IdUserCreation = App.user.IdUser;
            Session.Insert(session);
            CurrentSession = session;
            if (CurrentSession != null)
            {
                HomeViewModel.UpdateSessions();
                if (CurrentSession.IsRunning)
                {
                    HomeViewModel.RunningSession = CurrentSession;
                    HomeViewModel.StartSession();
                }
                    
                return true;
            }
            return false;
        }
    }
}

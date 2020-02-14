using AlienScale.Models;
using AlienScale.Views.CustomViews;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.ViewModels
{
    public class ToolbarViewModel : BaseViewModel
    {
        #region Properties
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
        private bool _isMenuOpen;
        public bool IsMenuOpen
        {
            get { return _isMenuOpen; }
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged("IsMenuOpen");
            }
        }
        private bool _isActivated;
        public bool IsActivated
        {
            get { return _isActivated; }
            set
            {
                _isActivated = value;
                OnPropertyChanged("IsActivated");
            }
        }
        private double _toolbarHeight;
        public double ToolbarHeight
        {
            get { return _toolbarHeight; }
            set
            {
                _toolbarHeight = value;
                OnPropertyChanged("ToolbarHeight");
            }
        }
        private string _buttonImageSource;
        public string ButtonImageSource
        {
            get { return _buttonImageSource; }
            set
            {
                _buttonImageSource = value;
                OnPropertyChanged("ButtonImageSource");
            }
        }
        private ICommand _animate;
        public ICommand Animate
        {
            get { return _animate; }
            set
            {
                _animate = value;
                OnPropertyChanged("Animate");
            }
        }
        private object _typePickerItemSelected;
        public object TypePickerItemSelected
        {
            get { return _typePickerItemSelected; }
            set
            {
                _typePickerItemSelected = value;
                OnPropertyChanged("TypePickerItemSelected");
            }
        }
        #endregion

        public ToolbarViewModel()
        {
            Animate = new Command((param) =>
            {
                IsActivated = true; 
            });

            IsActivated = false;
        }

        public async void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            if (CurrentSession != null)
            {
                //create coordinates
                Coordinate coor = new Coordinate() { IdCoordinate = Guid.NewGuid().ToString(), Lattitude = position.Latitude, Longitude = position.Longitude };

                Waypoint wp = new Waypoint() { Coordinate = coor, IdWaypoint = Guid.NewGuid().ToString() };

                //Getting waypoints to calculate order
                CurrentSession.FishingLoc.Waypoints = Waypoint.GetWaypointsByFishingLoc(CurrentSession.FishingLoc.IdFishingLoc);
                if (CurrentSession.FishingLoc.Waypoints != null || CurrentSession.FishingLoc.Waypoints.Count != 0)
                    wp.Order = CurrentSession.FishingLoc.Waypoints.Count + 1;
                else
                    wp.Order = 1;

                CurrentSession.FishingLoc.Waypoints.Add(wp);
                Session.InsertFullSession(CurrentSession);
            }
        }

        public async void ListenPosition()
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);

                if (status == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    locator.PositionChanged += Locator_PositionChanged;
                    await locator.StartListeningAsync(new TimeSpan(0), 100);
                }

            }
            catch (Exception ex)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged -= Locator_PositionChanged;
            }
        }
    }
}


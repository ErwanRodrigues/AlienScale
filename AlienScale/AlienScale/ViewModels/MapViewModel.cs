using AlienScale.Views.CustomViews;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace AlienScale.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
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
        
        public MapViewModel(HomeViewModel viewModel)
        {
            HomeViewModel = viewModel;
            async void getLocation()
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                CurrentMapSpan = new MapSpan(new Position(position.Latitude, position.Longitude), 0.1, 0.1);
            };
            getLocation();
        }
    }
}

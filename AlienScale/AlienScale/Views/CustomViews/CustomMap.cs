using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AlienScale.Views.CustomViews
{
    public class CustomMap : Map
    {
        //management of pin collection
        #region Items Property & management
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create("Items", typeof(IEnumerable<CustomPin>), typeof(CustomMap),
            null, propertyChanged: OnItemsChanged);

        public IEnumerable<CustomPin> Items
        {
            get { return (IEnumerable<CustomPin>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;

            if (oldValue is INotifyCollectionChanged)
                (oldValue as INotifyCollectionChanged).CollectionChanged -= map.OnCollectionChanged;
            if (newValue is INotifyCollectionChanged)
                (newValue as INotifyCollectionChanged).CollectionChanged += map.OnCollectionChanged;

            map.OnCollectionChanged(map, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            if (newValue != null)
                map.OnCollectionChanged(map, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)newValue));
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
                Pins.Clear();

            if (e.OldItems != null)
            {
                foreach (CustomPin pin in e.OldItems)
                {
                    Pins.Remove(pin.Pin);
                    pin.Pin.PropertyChanged -= OnPropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (CustomPin pin in e.NewItems)
                {
                    Pins.Add(pin.Pin);
                    pin.Pin.PropertyChanged += OnPropertyChanged;
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // We should be able to just replace the changed pin, but rebuild is required to force map refresh
            Pins.Clear();
            foreach (var pin in Items)
                Pins.Add(pin.Pin);
        }
        #endregion
        //management of current map span
        #region MapSpan Property & management
        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(propertyName: "MapSpan", returnType: typeof(MapSpan), declaringType: typeof(CustomMap),
            defaultValue: null, defaultBindingMode: BindingMode.TwoWay, validateValue: null, propertyChanged: MapSpanPropertyChanged);

        public MapSpan MapSpan
        {
            get { return (MapSpan)GetValue(MapSpanProperty); }
            set { SetValue(MapSpanProperty, value); }
        }

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as CustomMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }
        #endregion 
        //managament Click action on map
        #region On tap Action Property & management
        public static readonly BindableProperty OnTapActionProperty = BindableProperty.Create(propertyName: "OnTapAction", returnType: typeof(Action<Position>), declaringType: typeof(CustomMap),
            defaultValue: null, defaultBindingMode: BindingMode.OneWay, validateValue: null);
        
        public Action<Position> OnTapAction
        {
            get { return (Action<Position>)GetValue(OnTapActionProperty); }
            set { SetValue(OnTapActionProperty, value); }
        }
        
        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            MoveToRegion(new MapSpan(e.Position, 0.1, 0.1));
            //raise our custom event
            InvokeOnTapAction(e.Position);
        }

        public void InvokeOnTapAction(Position position)
        {
            if (OnTapAction == null || position == null)
            {
                return;
            }
            OnTapAction.Invoke(position);
        }
        #endregion
    }
    public class CustomPin
    {
        public Pin Pin { get; set; }
        public double Angle { get; set; }
        public string Url { get; set; }
        public bool ShowCallout { get; set; }
        public string Filename { get; set; }
        public List<Position> RouteCoordinates { get; set; }

        public CustomPin()
        {
            RouteCoordinates = new List<Position>();
        }
    }
    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienScale.Droid.CustomRenderers;
using AlienScale.Views.CustomViews;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace AlienScale.Droid.CustomRenderers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        IEnumerable<CustomPin> customPins;
        private GoogleMap _map;
        bool isDrawn;
        private Polyline _finalPolyline;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Cleanup();
            }
            base.Dispose(disposing);
        }

        private void Cleanup()
        {
            if (base.NativeMap == null) return;
            if (!Equals(_finalPolyline, null)) _finalPolyline.Remove();
            this.NativeMap.InfoWindowClick -= OnNativeMapInfoWindowClick;
            base.NativeMap.Clear();
            _map = null;
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            if (_map != null)
                _map.MapClick -= googleMap_MapClick;

            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.Items;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            // OnMapReady is called twice, not entirely certain why, known issue
            if (isDrawn) return;

            base.OnMapReady(map);
            _map = map;

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
            this.NativeMap.InfoWindowClick += OnNativeMapInfoWindowClick;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;

            isDrawn = true;
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));

            if (pin.Label.StartsWith("fish"))
            {
                string[] parameters = pin.Label.Split(";");
                string tempAddress = string.Empty;
                marker.SetTitle(parameters[1]);
                for (int i = 2; i < parameters.Count(); i++)
                {
                    tempAddress += parameters[i] + "\n";
                }
                marker.SetSnippet(tempAddress);
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_fish));
            }
            else if (pin.Label.StartsWith("location"))
            {
                string[] parameters = pin.Label.Split(";");
                string tempAddress = string.Empty;
                marker.SetTitle(parameters[1]);
                for (int i = 2; i < parameters.Count(); i++)
                {
                    tempAddress += parameters[i] + "\n";
                }
                marker.SetSnippet(tempAddress);
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_location));
            }
            else if (pin.Label.StartsWith("session"))
            {
                string[] parameters = pin.Label.Split(";");
                string tempAddress = string.Empty;
                marker.SetTitle(parameters[1]);
                for (int i = 2; i < parameters.Count(); i++)
                {
                    tempAddress += parameters[i] + "\n";
                }
                marker.SetSnippet(tempAddress);
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin_session));
            }
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            //draw route
            _finalPolyline = null;
            if (customPin.RouteCoordinates.Any())
            {
                var polylineOptions = new PolylineOptions();
                polylineOptions.InvokeColor(0x66FF0000);
                foreach (var coordinate in customPin.RouteCoordinates)
                {
                    polylineOptions.Add(new LatLng(coordinate.Latitude, coordinate.Longitude));
                }
                _finalPolyline = base.NativeMap.AddPolyline(polylineOptions);
            }

            if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                var url = Android.Net.Uri.Parse(customPin.Url);
                var intent = new Intent(Intent.ActionView, url);
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            if (customPins != null)

                foreach (var pin in customPins)
                {
                    if (pin.Pin.Position == position)
                    {
                        return pin;
                    }
                }
            return null;
        }

        public global::Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;

            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (customPin.Pin.Id.ToString() == "fish")
                {
                    view = inflater.Inflate(Resource.Layout.FishInfoWindow, null);
                }
                else if (customPin.Pin.Id.ToString() == "location")
                {
                    view = inflater.Inflate(Resource.Layout.LocationInfoWindow, null);
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.SessionInfoWindow, null);
                }

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public global::Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((CustomMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }

        private static void OnNativeMapInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs ev)
        {
            if (!Equals(ev.Marker, null))
                ev.Marker.HideInfoWindow();
        }

    }
}
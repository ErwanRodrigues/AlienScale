using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AlienScale.Models;

namespace AlienScale.Views.Toolbar
{
    public class BubbleView : ContentView
    {
        #region ItemsSource property  
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource), typeof(Session), typeof(BubbleView), null, propertyChanged: OnItemsSourceModified);
        public Session ItemSource
        {
            get
            {
                return (Session)GetValue(ItemSourceProperty);
            }
            set
            {
                SetValue(ItemSourceProperty, value);
            }
        }
        private static void OnItemsSourceModified(object sender, object oldValue, object newValue)
        {
            BubbleView bv = (BubbleView)sender;
            if (newValue != null)
            {
                Session sess = (Session)newValue;
                bv._kilos.Text = sess.TotalWeight.ToString();
                bv._time.Text = sess.Starts.ToString();
                bv._fishes.Text = sess.TotalCatches.ToString();
            }
        }
        #endregion
        AbsoluteLayout _mainLayout;
        Label _kilos;
        Label _time;
        Label _fishes;
        Label _rank;

        public BubbleView()
        {
            _mainLayout = new AbsoluteLayout()
            {

            };
            Image bubbles = new Image()
            {
                Source = "bubbles.png",
            };
            AbsoluteLayout.SetLayoutFlags(bubbles, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(bubbles, new Rectangle(0, 0, 1, 1));

            _kilos = new Label()
            {
                Text = "0.0 Kg",
            };
            AbsoluteLayout.SetLayoutFlags(_kilos, AbsoluteLayoutFlags.PositionProportional);
            //a préciser
            AbsoluteLayout.SetLayoutBounds(_kilos, new Rectangle(0.2, 0.4, -1, -1));

            _time = new Label()
            {
                Text = "22:18",
            };
            AbsoluteLayout.SetLayoutFlags(_time, AbsoluteLayoutFlags.PositionProportional);
            //a préciser
            AbsoluteLayout.SetLayoutBounds(_time, new Rectangle(0.4, 0.7, -1, -1));

            _fishes = new Label()
            {
                Text = "0 Fish",
            };
            AbsoluteLayout.SetLayoutFlags(_fishes, AbsoluteLayoutFlags.PositionProportional);
            //a préciser
            AbsoluteLayout.SetLayoutBounds(_fishes, new Rectangle(0.6, 0.4, -1, -1));

            _rank = new Label()
            {
                Text = "1/20",
                IsVisible = false,
            };
            AbsoluteLayout.SetLayoutFlags(_rank, AbsoluteLayoutFlags.PositionProportional);
            //a préciser
            AbsoluteLayout.SetLayoutBounds(_rank, new Rectangle(0.4, 0.7, -1, -1));

            _mainLayout.Children.Add(bubbles);
            _mainLayout.Children.Add(_kilos);
            _mainLayout.Children.Add(_time);
            _mainLayout.Children.Add(_fishes);
            _mainLayout.Children.Add(_rank);
            this.Content = _mainLayout;
        }
    }
}

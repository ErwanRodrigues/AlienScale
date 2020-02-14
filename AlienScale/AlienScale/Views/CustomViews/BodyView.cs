using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.Views.CustomViews
{
    public class BodyView : ContentView
    {
        #region ItemsSource property  
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(RibbonView), null, propertyChanged: OnItemsSourceModified);
        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        private static void OnItemsSourceModified(object sender, object oldValue, object newValue)
        {
            BodyView bv = (BodyView)sender;
            bv._mainLayout.Children.Clear();
            
            IEnumerator iter = ((IList)newValue).GetEnumerator();
            
            int index = 0;
            while (iter.MoveNext())
            {
                StackLayout layout = (StackLayout)iter.Current;
                bv._mainLayout.Children.Add(layout);
                ++index;
            }
        }
        #endregion
        #region Textcolor property  
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BodyView), Color.Black);
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }
        #endregion
        #region StyleProperty
        public static new readonly BindableProperty StyleProperty = BindableProperty.Create(nameof(Style), typeof(BodyView), typeof(RibbonView), null);
        public new Style Style
        {
            get
            {
                return (Style)GetValue(StyleProperty);
            }
            set
            {
                SetValue(StyleProperty, value);
            }
        }
        #endregion
        #region Barcolor property  
        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(nameof(BarColor), typeof(Color), typeof(BodyView), Color.Black);
        public Color BarColor
        {
            get
            {
                return (Color)GetValue(BarColorProperty);
            }
            set
            {
                SetValue(BarColorProperty, value);
            }
        }
        #endregion
        #region FontSize property  
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(BodyView), 13);
        public int FontSize
        {
            get
            {
                return (int)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }
        #endregion

        double _originalX;
        double _originalY;
        double _originalHeight;

        //public event EventHandler ItemSwipped = (e, a) => { };

        StackLayout _mainLayout;
        public BodyView()
        {
            /*this.GestureRecognizers.Add(new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Left,
                Command = new Command((obj)=>
                {
                    ItemSwipped(this, new SwipeEventArgs() { Direction = SwipeDirection.Left });
                })
            });
            GestureRecognizers.Add(new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Right,
                Command = new Command((obj) =>
                {

                    ItemSwipped(this, new SwipeEventArgs() { Direction = SwipeDirection.Right });
                })
            });*/

            _mainLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0),
                Spacing = 0
            };
            Content = _mainLayout;
        }
    }
    /*public class SwipeEventArgs : EventArgs
    {
        public SwipeDirection Direction { get; set; }
    }*/
}

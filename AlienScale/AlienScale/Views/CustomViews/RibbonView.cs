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
    public class RibbonView : ContentView
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
            RibbonView rv = (RibbonView)sender;
            rv._itemsContainerLayout.Children.Clear();
            IEnumerator iter = ((IList)newValue).GetEnumerator();

            int index = 0;
            while (iter.MoveNext())
            {
                int i = 0;
                String label = (String)iter.Current;
                StackLayout layout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(10, 10, 10, 0),
                };
                Image icon = new Image()
                {
                    Source = label + ".png",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 30,
                };
                Label titleLabel = new Label()
                {
                    Text = label,
                    TextColor = rv.TextColor,
                    FontSize = rv.FontSize,
                    Style = rv.Style,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                BoxView box = new BoxView()
                {
                    BackgroundColor = rv.BarColor,
                    VerticalOptions = LayoutOptions.EndAndExpand,
                    HeightRequest = 2,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                BoxView boxExt = new BoxView()
                {
                    VerticalOptions = LayoutOptions.EndAndExpand,
                    HeightRequest = 2,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                layout.Children.Add(icon);
                layout.Children.Add(titleLabel);
                layout.Children.Add(box);
                layout.Children.Add(boxExt);
                layout.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command((obj) => {
                        rv.OnSelectedItem(label);
                    })
                });
                i++;
                ++index;
                rv._itemsContainerLayout.Children.Add(layout);
            }
            rv.OnSelectedItem(((List<String>)rv.ItemsSource).ElementAt(rv.SelectedItemIndex));
        }
        #endregion
        #region Textcolor property  
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RibbonView), Color.Black);
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
        public static new readonly BindableProperty StyleProperty = BindableProperty.Create(nameof(Style), typeof(Style), typeof(RibbonView), null);
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
        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(nameof(BarColor), typeof(Color), typeof(RibbonView), Color.Black);
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
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(int), typeof(RibbonView), 13);
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
        #region SelectedItemIndex property  
        public static readonly BindableProperty SelectedItemIndexProperty = BindableProperty.Create(nameof(SelectedItemIndex), typeof(int), typeof(RibbonView), null, BindingMode.TwoWay, null, OnIndexSelectedPropertyChanged);
        public int SelectedItemIndex
        {
            get
            {
                return (int)GetValue(SelectedItemIndexProperty);
            }
            set
            {
                SetValue(SelectedItemIndexProperty, value);
            }
        }
        private static void OnIndexSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            int i = 0;
            var thisInstance = bindable as RibbonView;
            if (newValue != null)
            {
                thisInstance.OnSelectedItem(((List<String>)thisInstance.ItemsSource).ElementAt(thisInstance.SelectedItemIndex));
            }
        }
        #endregion
        #region ItemSelected property  
        public static readonly BindableProperty ItemSelectedProperty = BindableProperty.Create(nameof(ItemSelected), typeof(ICommand), typeof(RibbonView), null);
        public ICommand ItemSelected
        {
            get
            {
                return (ICommand)GetValue(ItemSelectedProperty);
            }
            set
            {
                SetValue(ItemSelectedProperty, value);
            }
        }
        #endregion

        StackLayout _mainLayout;
        StackLayout _itemsContainerLayout;
        public RibbonView()
        {
            _mainLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0),
                Spacing = 0
            };
            _itemsContainerLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5, 0, 5, 0),
                Spacing = 0
            };
            _mainLayout.Children.Add(_itemsContainerLayout);
            Content = _mainLayout;
        }

        private void OnSelectedItem(String labelTitle)
        {
            int i = 0;
            IEnumerator iter = ItemsSource.GetEnumerator();
            if (this.SelectedItemIndex > -1)
            {
                StackLayout itemStack = (StackLayout)_itemsContainerLayout.Children.ToArray()[this.SelectedItemIndex];
                BoxView bv = (BoxView)itemStack.Children.ToArray()[2];
                bv.BackgroundColor = this.BackgroundColor;
            }
            while (iter.MoveNext())
            {
                StackLayout itemStack = (StackLayout)_itemsContainerLayout.Children.ToArray()[i];
                BoxView bv = (BoxView)itemStack.Children.ToArray()[2];
                if (((string)iter.Current).CompareTo(labelTitle) == 0)
                {
                    bv.BackgroundColor = this.BarColor;
                    this.SelectedItemIndex = i;
                }
                else
                {
                    bv.BackgroundColor = this.BackgroundColor;
                }
                i += 1;
            }
            if (ItemSelected != null)
            {
                ItemSelected.Execute(this.SelectedItemIndex);
            }
        }
    }
    
}

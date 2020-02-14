using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuSlideView : ContentView
    {
        #region DefaultHeightProperty
        public static readonly BindableProperty DefaultWidthProperty =
            BindableProperty.Create("DefaultWidth", typeof(double), typeof(MenuSlideView), 0.0, BindingMode.TwoWay, null, DefaultWidthPropertyChanged);

        public double DefaultWidth
        {
            get
            {
                return (double)GetValue(DefaultWidthProperty);
            }
            set
            {
                SetValue(DefaultWidthProperty, value);
            }
        }

        private static void DefaultWidthPropertyChanged(object bindableObject, object oldValue, object newValue)
        {
            (bindableObject as MenuSlideView).IsVisible = false;
            (bindableObject as MenuSlideView).TranslationX = (double)newValue;
        }
        #endregion

        #region IsSlideOpenProperty
        public static readonly BindableProperty IsSlideOpenProperty =
            BindableProperty.Create("IsSlideOpen", typeof(bool), typeof(MenuSlideView), false, BindingMode.TwoWay, null, IsSlideOpenPropertyChanged);

        public double IsSlideOpen
        {
            get
            {
                return (double)GetValue(IsSlideOpenProperty);
            }
            set
            {
                SetValue(IsSlideOpenProperty, value);
            }
        }

        private static async void IsSlideOpenPropertyChanged(object bindableObject, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                (bindableObject as MenuSlideView).IsVisible = true;
                await (bindableObject as MenuSlideView).TranslateTo(0, 0, 250, Easing.SinInOut);
                newValue = false;
            }
            else
            {
                await (bindableObject as MenuSlideView).TranslateTo(App.Current.MainPage.Width, 0, 250, Easing.SinInOut);
                (bindableObject as MenuSlideView).IsVisible = false;
                newValue = true;
            }
        }

        #endregion

        #region IsSlideOpenProperty
        public static readonly BindableProperty ItemsListProperty =
            BindableProperty.Create("ItemsList", typeof(IList<MenuItem>), typeof(MenuSlideView), null, BindingMode.TwoWay, null, ItemsListPropertyChanged);

        public double ItemsList
        {
            get
            {
                return (double)GetValue(ItemsListProperty);
            }
            set
            {
                SetValue(ItemsListProperty, value);
            }
        }

        private static async void ItemsListPropertyChanged(object bindableObject, object oldValue, object newValue)
        {

        }

        #endregion

        public MenuSlideView()
        {
            InitializeComponent();
        }
    }
}

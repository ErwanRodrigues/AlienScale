using AlienScale.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlienScale.Views.CustomViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentView
	{
        #region bindable properties
        #region LayoutCompressedLevelProperty
        public static readonly BindableProperty LayoutCompressedLevelProperty =
            BindableProperty.Create("LayoutCompressedLevel", typeof(int), typeof(HomeView), 0, BindingMode.TwoWay, null, OnLayoutCompressedLevelChanged);

        public int LayoutCompressedLevel
        {
            get
            {
                return (int)GetValue(LayoutCompressedLevelProperty);
            }
            set
            {
                SetValue(LayoutCompressedLevelProperty, value);
            }
        }
        private async static void OnLayoutCompressedLevelChanged(object sender, object oldValue, object newValue)
        {
            HomeView cntView = (HomeView)sender;
            int lvl = (int)newValue;
            int olvl = (int)oldValue;
            await cntView.AnimateLayout(lvl, olvl);
        }
        #endregion
        #region CommandProperty
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(HomeView), null, BindingMode.TwoWay, null);

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }
        #endregion
        #region CommandParameterProperty
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(HomeView), null, BindingMode.TwoWay, null);

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }
        #endregion
        #endregion

        double _originalHeight = 0;
        double _originalAddButtonX = 0.0;
        double _originalAddButtonY = 0.0;
        double _level1 = (Device.Info.ScaledScreenSize.Height * 0.020);
        double _level2 = (Device.Info.ScaledScreenSize.Height * 0.15);
        double _level3 = (Device.Info.ScaledScreenSize.Height * 0.1);

        public HomeView ()
		{
			InitializeComponent ();
            FloatingAddButton.Tapped += FireAddTapCommand;
        }

        private void FireAddTapCommand(object sender, EventArgs e)
        {
            //AnimateAddButtonOut();
            this.Command?.Execute(e);
        }

        #region animations
        /*private async void AnimateAddButtonIn()
        {
            await FloatingAddButton.FadeTo(1.0, 250, Easing.Linear);
            FloatingAddButton.ScaleTo(1.0, 250, Easing.SinInOut);
            
        }

        private async void AnimateAddButtonOut()
        {
            _originalAddButtonX = FloatingAddButton.Bounds.X;
            _originalAddButtonY = FloatingAddButton.Bounds.Y;

            await FloatingAddButton.TranslateTo((Device.Info.ScaledScreenSize.Width - FloatingAddButton.X) - Device.Info.ScaledScreenSize.Width / 2 - FloatingAddButton.Width / 2, (Device.Info.ScaledScreenSize.Height - FloatingAddButton.Y) - Device.Info.ScaledScreenSize.Height / 2 - FloatingAddButton.Height / 2);
            FloatingAddButton.ScaleTo(4.0, 250, Easing.SinInOut);
            await FloatingAddButton.FadeTo(0.0, 250, Easing.Linear);
        }*/

        private async Task<bool> AnimateLayout(int lvl, int olvl)
        {
            var r = this.Bounds;
            _originalHeight = r.Height;
            if (lvl == 1 && olvl == 0)
            {
                r.Bottom = _originalHeight - _level1;
                await AnimateLayoutUp(r);
            }
            else if(lvl == 2 && olvl == 1)
            {
                r.Bottom = _originalHeight + _level1 - _level2;
                await AnimateLayoutUp(r);
            }
            else if(lvl == 1 && olvl == 2)
            {
                r.Bottom = _originalHeight + _level2 - _level1;
                await AnimateLayoutDown(r);
            }
            else if(lvl == 1 && olvl == 3)
            {
                r.Bottom = _originalHeight + _level3 - _level1;
                await AnimateLayoutDown(r);
            }
            else if (lvl == 0 && olvl == 1)
            {
                r.Bottom = _originalHeight + _level1;
                await AnimateLayoutDown(r);
            }
            else if (lvl == 0 && olvl == 2)
            {
                r.Bottom = _originalHeight + _level2;
                await AnimateLayoutDown(r);
            }
            else if (lvl == 0 && olvl == 3)
            {
                r.Bottom = _originalHeight + _level3;
                await AnimateLayoutDown(r);
            }
            else if (lvl == 3 && olvl == 1)
            {
                //specific for the map view 
                r.Bottom = _originalHeight + _level1 - _level3;
                await AnimateLayoutUp(r);
            }
            return true;
        }

        private async Task<bool> AnimateLayoutUp(Rectangle bnds)
        {
            await this.LayoutTo(bnds, 300);
            return true;
        }

        private async Task<bool> AnimateLayoutDown(Rectangle bnds)
        {
            await Task.Delay(100).ContinueWith(t =>
            {
                this.LayoutTo(bnds, 270);
            });
            return true;
        }
        #endregion
    }
}
using AlienScale.ViewModels;
using System.Collections;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AlienScale.StaticResources;

namespace AlienScale.Views.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFloatingButton : ContentView
    {
        #region IsVisibleProperty
        public static readonly BindableProperty IsButtonVisibleProperty =
            BindableProperty.Create("IsButtonVisible", typeof(bool), typeof(ImageButton), false, BindingMode.TwoWay, null, OnVisibilityChanged);

        public bool IsButtonVisible
        {
            get
            {
                return (bool)GetValue(IsButtonVisibleProperty);
            }
            set
            {
                SetValue(IsButtonVisibleProperty, value);
            }
        }

        private static void OnVisibilityChanged(object sender, object oldValue, object newValue)
        {
            CustomFloatingButton cntView = (CustomFloatingButton)sender;
            if ((bool)newValue)
            {
                cntView.IsVisible = true;
                cntView.InitializeButton();
            }
            else
            {
                cntView.IsVisible = false;
            }
        }
        #endregion
        private bool _isOpenMenu = false;
        private double _originalLeft;
        private double _originalWidth;

        public CustomFloatingButton()
        {
            InitializeComponent();
            GestureRecognizers.Add(new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Up,
                Command = new Command((obj) =>
                {
                    ToggleActionBarMenu();
                })
            });

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command((obj) =>
                {
                    ToggleActionBarMenu();
                })
            });

        }

        private void InitializeButton()
        {
            Task.Delay(200).ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ButtonLayout.TranslationY = this.Height;
                    ButtonLayout.IsVisible = true;
                    ButtonLayout.TranslationX = (Width / 2) - this.Height / 2;
                    ButtonLayout.TranslateTo(0, -(this.Height / 6), 450).ContinueWith(w =>
                    {
                        ButtonLayout.ScaleTo(1.4, 300, Easing.Linear).ContinueWith(y =>
                        {
                            ButtonLayout.ScaleTo(1.0, 200, Easing.Linear).ContinueWith(k =>
                            {
                                Task.Delay(300).ContinueWith(z =>
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        AnimateButtonOut();
                                    });
                                });
                            });
                        });
                    });
                });
            });
        }

        private void ToggleActionBarMenu()
        {
            if (!_isOpenMenu)
            {
                OpenMenu();
                _isOpenMenu = true;
            }
            else
            {
                CloseMenu();
                _isOpenMenu = false;
            }
        }

        private void CloseMenu()
        {
            MenuLayout.TranslateTo(0, MenuLayout.Height, 20);
            MenuLayout.FadeTo(0.2);

            AnimateBlueBarOut().ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    BlueBarLayout.TranslationY = BlueBarLayout.Height;
                    ButtonLayout.IsVisible = true;

                    AnimateButtonOut();
                });
            });
        }

        private void OpenMenu()
        {
            BlueBarLayout.IsVisible = true;
            MenuLayout.IsVisible = true;

            AnimateButtonIn().ContinueWith(e =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    BlueBarLayout.TranslationY = 0;
                    ButtonLayout.IsVisible = false;

                    AnimateBlueBarIn().ContinueWith(t =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MenuLayout.TranslateTo(0, 0);
                            MenuLayout.FadeTo(0.75);
                        });
                    });
                });
            });
        }

        private Task AnimateButtonIn()
        {
            return this.TranslateTo(0, 0, 150);
        }

        private Task AnimateButtonOut()
        {
            var y = (3 * this.Height) / 4;
            var x = 0;
            return this.TranslateTo(x, y, 250);
        }

        private Task AnimateBlueBarIn()
        {
            var r = BlueBarLayout.Bounds;
            _originalLeft = r.Left;
            _originalWidth = r.Width;

            r.Width = MenuLayout.Width + 5;
            r.Left = r.Left - ((Width - _originalWidth) / 2);
            return BlueBarLayout.LayoutTo(r, 150);
        }

        private Task AnimateBlueBarOut()
        {
            var r = BlueBarLayout.Bounds;
            r.Width = _originalWidth;
            r.Left = _originalLeft;
            return BlueBarLayout.LayoutTo(r, 150);
        }

        public void Tapped()
        {
            if(_isOpenMenu)
                ToggleActionBarMenu();
        }
        public void DragUp()
        {
            if(!_isOpenMenu)
                ToggleActionBarMenu();
        }
    }
}
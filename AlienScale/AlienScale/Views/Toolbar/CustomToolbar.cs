using AlienScale.Models;
using AlienScale.Views.CustomControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.Views.Toolbar
{
    public class CustomToolbar : ContentView
    {
        #region ItemsSource property  
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource), typeof(Session), typeof(CustomToolbar), null, propertyChanged: OnItemsSourceModified);
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
            CustomToolbar bv = (CustomToolbar)sender;
            if (newValue != null)
            {
                bv._bubbleView.ItemSource = (Session)newValue;
            }
        }
        #endregion
        #region AddFishCommandProperty
        public static readonly BindableProperty AddFishCommandProperty =
            BindableProperty.Create("AddFishCommand", typeof(ICommand), typeof(CustomToolbar), null, BindingMode.TwoWay, null);

        public ICommand AddFishCommand
        {
            get
            {
                return (ICommand)GetValue(AddFishCommandProperty);
            }
            set
            {
                SetValue(AddFishCommandProperty, value);
            }
        }
        #endregion
        #region ViewSessionCommandProperty
        public static readonly BindableProperty ViewSessionCommandProperty =
            BindableProperty.Create("ViewSessionCommand", typeof(ICommand), typeof(CustomToolbar), null, BindingMode.TwoWay, null);

        public ICommand ViewSessionCommand
        {
            get
            {
                return (ICommand)GetValue(ViewSessionCommandProperty);
            }
            set
            {
                SetValue(ViewSessionCommandProperty, value);
            }
        }
        #endregion
        #region StopSessionCommandProperty
        public static readonly BindableProperty StopSessionCommandProperty =
            BindableProperty.Create("ViewSessionCommand", typeof(ICommand), typeof(CustomToolbar), null, BindingMode.TwoWay, null);

        public ICommand StopSessionCommand
        {
            get
            {
                return (ICommand)GetValue(StopSessionCommandProperty);
            }
            set
            {
                SetValue(StopSessionCommandProperty, value);
            }
        }
        #endregion
        #region ToolbarHeightProperty
        public static readonly BindableProperty ToolbarHeightProperty =
            BindableProperty.Create("ToolbarHeight", typeof(double), typeof(CustomToolbar), 0.0, BindingMode.TwoWay, null, OnToolbarHeightPropertyChanged);

        public double ToolbarHeight
        {
            get
            {
                return (double)GetValue(ToolbarHeightProperty);
            }
            set
            {
                SetValue(ToolbarHeightProperty, value);
            }
        }

        private static void OnToolbarHeightPropertyChanged(object sender, object oldValue, object newValue)
        {
            var thisInstance = (CustomToolbar)sender;
            double hgt = (double)newValue;
            if (hgt > 0)
            {
                thisInstance.InitializePosition(hgt);
            }
            else
            {
                thisInstance.InitializePosition(72.0);
            }
        }
        private void InitializePosition(double height)
        {

        }
        #endregion
        #region IsMenuOpenProperty
        public static readonly BindableProperty IsMenuOpenProperty =
            BindableProperty.Create("IsMenuOpen", typeof(bool), typeof(CustomToolbar), false, BindingMode.TwoWay, null, OnIsMenuOpenPropertyChanged);

        public bool IsMenuOpen
        {
            get
            {
                return (bool)GetValue(IsMenuOpenProperty);
            }
            set
            {
                SetValue(IsMenuOpenProperty, value);
            }
        }
        private static void OnIsMenuOpenPropertyChanged(object sender, object oldValue, object newValue)
        {
            CustomToolbar cntView = (CustomToolbar)sender;
            if((bool)newValue == false)
            {
                cntView.CloseMenu();
            }
        }
        #endregion
        #region IsToolbarVisibleProperty
        public static readonly BindableProperty IsToolbarVisibleProperty =
            BindableProperty.Create("IsToolbarVisible", typeof(bool), typeof(CustomToolbar), false, BindingMode.TwoWay, null, OnVisibilityChanged);

        public bool IsToolbarVisible
        {
            get
            {
                return (bool)GetValue(IsToolbarVisibleProperty);
            }
            set
            {
                SetValue(IsToolbarVisibleProperty, value);
            }
        }

        private static void OnVisibilityChanged(object sender, object oldValue, object newValue)
        {
            CustomToolbar cntView = (CustomToolbar)sender;
            if ((bool)newValue)
            {
                cntView.IsVisible = true;
                cntView.InitializeToolbar();
                Task.Run(() => cntView.AnimateWaterHorizontal());
            }
            else
            {
                cntView.CloseMenu();
                cntView.TranslationY=150;
                cntView.IsVisible = false;
            }
        }
        #endregion

        AbsoluteLayout _mainLayout;
        BubbleView _bubbleView;
        CustomImageButton _fishButton;
        StackLayout _menuLayout;
        Image _water1;
        Image _water2;
        Image _water3;
        public CustomToolbar()
        {
            _mainLayout = new AbsoluteLayout()
            {
                BackgroundColor = Color.Transparent,
            };
            //adding all the backgrounds
            Image bubblesBackground = new Image()
            {
                Source = "bubblesbackground.png",
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                IsVisible = false,
                InputTransparent = true,
            };
            AbsoluteLayout.SetLayoutFlags(bubblesBackground, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(bubblesBackground, new Rectangle(0, 0, 1, 1));

            _water3 = new Image()
            {
                Source = "water3.png",
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                InputTransparent = true,
            };
            AbsoluteLayout.SetLayoutFlags(_water3, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.HeightProportional);
            AbsoluteLayout.SetLayoutBounds(_water3, new Rectangle(0.5, 0, 3 * Device.Info.PixelScreenSize.Width, 1));
            
            _water2 = new Image()
            {
                Source = "water2.png",
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                InputTransparent = true,
            };
            AbsoluteLayout.SetLayoutFlags(_water2, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.HeightProportional);
            AbsoluteLayout.SetLayoutBounds(_water2, new Rectangle(0.5, 0, 3 * Device.Info.PixelScreenSize.Width, 1));

            _water1 = new Image()
            {
                Source = "water1.png",
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                InputTransparent = true,
            };
            AbsoluteLayout.SetLayoutFlags(_water1, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.HeightProportional);
            AbsoluteLayout.SetLayoutBounds(_water1, new Rectangle(0.5, 0, 3 * Device.Info.PixelScreenSize.Width, 1));

            //creating menu layout
            _menuLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                TranslationY = 110,
            };
            AbsoluteLayout.SetLayoutFlags(_menuLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(_menuLayout, new Rectangle(0, 0, 1, 1));

            Grid menuGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
            };
            ColumnDefinition c1 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) };
            ColumnDefinition c2 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) };
            ColumnDefinition c3 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) };
            ColumnDefinition c4 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };

            menuGrid.ColumnDefinitions.Add(c1);
            menuGrid.ColumnDefinitions.Add(c2);
            menuGrid.ColumnDefinitions.Add(c3);
            menuGrid.ColumnDefinitions.Add(c4);
            
            CustomImageButton addFish = new CustomImageButton()
            {
                Source = "addfish.png",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(10, 30, 10, 0),
            };
            addFish.FadeTo(0.85);
            CustomImageButton viewSession = new CustomImageButton()
            {
                Source = "viewsession.png",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(10, 30, 10, 0)
            };
            viewSession.FadeTo(0.85);

            CustomImageButton stopSession = new CustomImageButton()
            {
                Source = "stopsession.png",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(10, 30, 10, 0)
            };
            stopSession.FadeTo(0.85);

            _bubbleView = new BubbleView()
            {
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 30, 10, 10),
                InputTransparent = true,
            };
            _bubbleView.FadeTo(0.85);

            menuGrid.Children.Add(addFish, 0, 0);
            menuGrid.Children.Add(viewSession, 1, 0);
            menuGrid.Children.Add(stopSession, 2, 0);
            menuGrid.Children.Add(_bubbleView, 3, 0);

            _menuLayout.Children.Add(menuGrid);

            //creating fish button
            _fishButton = new CustomImageButton()
            {
                Source = "thon_center.png",
                BackgroundColor = Color.Transparent,
            };
            AbsoluteLayout.SetLayoutFlags(_fishButton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(_fishButton, new Rectangle(0.5, 0, -1, -1));

            _mainLayout.Children.Add(_water3);
            _mainLayout.Children.Add(_water2);
            _mainLayout.Children.Add(_water1);
            _mainLayout.Children.Add(bubblesBackground);
            _mainLayout.Children.Add(_menuLayout);
            _mainLayout.Children.Add(_fishButton);

            this.Content = _mainLayout;
            this.TranslationY = 150;
            this.IsVisible = false;
            //event handlers 
            _fishButton.ItemSwipped += ToggleMenu;
            addFish.ItemTapped += FireAddFishCommand;
            viewSession.ItemTapped += FireViewSessionCommand;
            stopSession.ItemTapped += FireStopSessionCommand;
        }

        private void FireAddFishCommand(object sender, EventArgs e)
        {
            CustomImageButton cib = (CustomImageButton)sender;
            this.AddFishCommand?.Execute(sender);
        }
        private void FireViewSessionCommand(object sender, EventArgs e)
        {
            CustomImageButton cib = (CustomImageButton)sender;
            this.ViewSessionCommand?.Execute(sender);
        }
        private void FireStopSessionCommand(object sender, EventArgs e)
        {
            CustomImageButton cib = (CustomImageButton)sender;
            this.StopSessionCommand?.Execute(sender);
        }

        public void ToggleMenu(object sender, EventArgs e)
        {
            IsMenuOpen = !IsMenuOpen;
            if (IsMenuOpen)
                OpenMenu();
            else
                CloseMenu();
        }

        #region Animations
        private void InitializeToolbar()
        {
            //set original position foir waters images
            _water1.TranslationX = 0;
            _water2.TranslationX = 0;
            _water3.TranslationX = 0;
            int count = 0;
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                _fishButton.IsVisible = true;
                _fishButton.InputTransparent = false;
                await this.TranslateTo(0, 0, 600);
                await _mainLayout.TranslateTo(0, 0, 150);
                await ButtonIntro();
                await this.TranslateTo(0, 70, 300);
            });

            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                if (IsToolbarVisible)
                {
                    if (count % 100 == 0 && count > 99)
                    {
                        SwitchFishLeftRight();
                    }
                    count++;
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
        private async Task<bool> ButtonIntro()
        {
            await _fishButton.TranslateTo(0, -110, 300);
            await _fishButton.ScaleTo(1.2, 150);
            await Task.Delay(300);
            await SwitchFishLeftRight();
            await SwitchFishLeftRight();
            await Task.Delay(400);
            await _fishButton.ScaleTo(1.0, 150);
            await _fishButton.TranslateTo(0, 0, 300);
            return true;
        }
        private async Task<bool> SwitchFishLeftRight()
        {
            Device.BeginInvokeOnMainThread(() => { _fishButton.Source = ImageSource.FromFile("thon_right0"); });
            await Task.Delay(40);
            Device.BeginInvokeOnMainThread(() => { _fishButton.Source = ImageSource.FromFile("thon_right1"); });
            await Task.Delay(90);
            Device.BeginInvokeOnMainThread(() => { _fishButton.Source = ImageSource.FromFile("thon_center"); });
            await Task.Delay(25);
            Device.BeginInvokeOnMainThread(() => { _fishButton.Source = ImageSource.FromFile("thon_left0"); });
            await Task.Delay(40);
            Device.BeginInvokeOnMainThread(() => { _fishButton.Source = ImageSource.FromFile("thon_left1"); });
            await Task.Delay(90);
            Device.BeginInvokeOnMainThread(() => { _fishButton.Source = ImageSource.FromFile("thon_center"); });
            return true;
        }

        private void OpenMenu()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.TranslateTo(0, 0, 300).ContinueWith(a =>
                {
                    _fishButton.InputTransparent = true;
                    SwitchFishLeftRight();
                    _fishButton.FadeTo(0.0, 300).ContinueWith(b =>
                    {
                        _menuLayout.TranslateTo(0, 0, 300);
                    });
                });
            });
        }

        private void CloseMenu()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _menuLayout.TranslateTo(0, 110, 300);
                _fishButton.IsVisible = true;
                _fishButton.FadeTo(1.0, 300);
                _fishButton.InputTransparent = false;
                this.TranslateTo(0, 70, 300);
            });
        }

        private void AnimationTest()
        {
            //ca fonctionne
            var parentAnimation = new Animation();
            var buttonLayoutTranslation = new Animation(v => _fishButton.TranslationY = v, 0, -250, Easing.Linear);
            parentAnimation.Add(0, 0.5, buttonLayoutTranslation);
            parentAnimation.Commit(this, "ChildAnimations", 16, 500, null, (v, c) => CloseMenu());
        }

        private void AnimateWaterHorizontal()
        {
            double x1 = 0;
            double x2 = 0;
            double x3 = 0;
            double y1 = 0;
            bool toRight = true;
            bool upState = false;
            //water front animation
            //as far as the timer is 1 sec long, when we make the toolbar unvisible, we need to wait at least 1 sec to stop the timer
            Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            {
                if (IsToolbarVisible)
                {
                    //animate horizontal
                    if (toRight)
                        x1 += 5;
                    else
                        x1 -= 5;

                    //animate vertical 
                    if (upState)
                    {
                        y1 += 2;
                        upState = !upState;
                    }
                    else
                    {
                        y1 -= 2;
                        upState = !upState;
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _water1.TranslateTo(x1, y1, 500);
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            });

            //water 2nd plan animation
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                if (IsToolbarVisible)
                {
                    if (toRight)
                        x2++;
                    else
                        x2--;

                    if (x2 >= Device.Info.PixelScreenSize.Width || x2 <= -Device.Info.PixelScreenSize.Width)
                    {
                        toRight = !toRight;
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _water2.TranslateTo(x2, 0, 10);
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            });
            //water 3rd plan animation
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                if (IsToolbarVisible)
                {
                    if (toRight)
                        x3--;
                    else
                        x3++;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _water3.TranslateTo(x3, 0, 10);
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
        #endregion
    }
}

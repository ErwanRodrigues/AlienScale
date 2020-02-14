using AlienScale.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.Views.CustomControls
{
    public class CustomFloatingAddButton : ContentView
    {
        #region Bindable properties
        #region DragDirectionProperty
        public static readonly BindableProperty DragDirectionProperty = BindableProperty.Create(
           propertyName: "DragDirection",
           returnType: typeof(DragDirectionType),
           declaringType: typeof(CustomFloatingAddButton),
           defaultValue: DragDirectionType.All,
           defaultBindingMode: BindingMode.TwoWay);

        public DragDirectionType DragDirection
        {
            get { return (DragDirectionType)GetValue(DragDirectionProperty); }
            set { SetValue(DragDirectionProperty, value); }
        }

        #endregion
        #region DragModeProperty 
        public static readonly BindableProperty DragModeProperty = BindableProperty.Create(
           propertyName: "DragMode",
           returnType: typeof(DragMode),
           declaringType: typeof(CustomFloatingAddButton),
           defaultValue: DragMode.LongPress,
           defaultBindingMode: BindingMode.TwoWay);

        public DragMode DragMode
        {
            get { return (DragMode)GetValue(DragModeProperty); }
            set { SetValue(DragModeProperty, value); }
        }
        #endregion
        #region IsDraggingProperty 
        public static readonly BindableProperty IsDraggingProperty = BindableProperty.Create(
          propertyName: "IsDragging",
          returnType: typeof(bool),
          declaringType: typeof(CustomFloatingAddButton),
          defaultValue: false,
          defaultBindingMode: BindingMode.TwoWay);

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }
        #endregion
        #region RestorePositionCommandProperty
        public static readonly BindableProperty RestorePositionCommandProperty = BindableProperty.Create(
            nameof(RestorePositionCommand), 
            typeof(ICommand), 
            typeof(CustomFloatingAddButton), 
            default(ICommand), 
            BindingMode.TwoWay, 
            null, 
            OnRestorePositionCommandPropertyChanged);

        static void OnRestorePositionCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = bindable as CustomFloatingAddButton;
            if (source == null)
            {
                return;
            }
            source.OnRestorePositionCommandChanged();
        }

        private void OnRestorePositionCommandChanged()
        {
            OnPropertyChanged("RestorePositionCommand");
        }

        public ICommand RestorePositionCommand
        {
            get
            {
                return (ICommand)GetValue(RestorePositionCommandProperty);
            }
            set
            {
                SetValue(RestorePositionCommandProperty, value);
            }
        }
        #endregion
        #region IsButtonVisibleProperty 
        public static readonly BindableProperty IsButtonVisibleProperty = BindableProperty.Create(
          propertyName: "IsButtonVisible",
          returnType: typeof(bool),
          declaringType: typeof(CustomFloatingAddButton),
          defaultValue: true,
          defaultBindingMode: BindingMode.TwoWay,
          validateValue: null,
          propertyChanged: OnButtonVisibilityChanged);

        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        static void OnButtonVisibilityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = bindable as CustomFloatingAddButton;
            if ((bool)newValue)
            {
                source.IsVisible = true;
                //source.AnimateButtonIn();
            }
            else
            {
                //source.AnimateButtonOut();
                source.IsVisible = false;
            }
        }
        #endregion
        #endregion

        Image _image;
        public CustomFloatingAddButton()
        {
            StackLayout _layout = new StackLayout()
            {
                BackgroundColor = Color.Transparent,
            };
            _image = new Image()
            {
                Source = "addround.png",
            };
            _layout.Children.Add(_image);
            this.Content = _layout;
        }

        #region Events
        public event EventHandler DragStart = delegate { };
        public event EventHandler DragEnd = delegate { };
        public event EventHandler Tapped = delegate { };

        public void DragStarted()
        {
            DragStart(this, default(EventArgs));
            IsDragging = true;
        }

        public void DragEnded()
        {
            IsDragging = false;
            DragEnd(this, default(EventArgs));
        }
        public void Tap()
        {
            IsDragging = false;
            Tapped(this, default(EventArgs));
        }
        #endregion

        #region Animation
        /*private async void AnimateButtonOut()
        {
            await this.TranslateTo((Device.Info.ScaledScreenSize.Width - this.X) - Device.Info.ScaledScreenSize.Width / 2 - this.Width / 2, (Device.Info.ScaledScreenSize.Height - this.Y) - Device.Info.ScaledScreenSize.Height / 2 - this.Height / 2);
            this.ScaleTo(4.0, 250, Easing.SinInOut);
            await this.FadeTo(0.0, 250, Easing.Linear);
        }

        private async void AnimateButtonIn()
        {
            await this.FadeTo(1.0, 250, Easing.Linear);
            this.ScaleTo(1.0, 250, Easing.SinInOut);
            this.RestorePositionCommand.Execute(null);
        }*/

        private void ButtonRotation()
        {
            int count = 0;
            double angle = 0.0;
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                if (count % 100 == 0 && count > 99)
                {
                    angle += 90;
                    this._image.RotateTo(angle, 125, Easing.SinInOut);
                    if (angle > 270)
                    {
                        angle = 0;
                        this._image.RotateTo(angle, 50, Easing.SinInOut);
                    }
                }
                count++;
                return true;
            });
        }
        #endregion

    }
}

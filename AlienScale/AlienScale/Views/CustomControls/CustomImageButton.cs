using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.Views.CustomControls
{
    public class CustomImageButton : Image
    {

        #region CommandProperty
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomImageButton), null, BindingMode.TwoWay, null);

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
            BindableProperty.Create("Command", typeof(object), typeof(CustomImageButton), null, BindingMode.TwoWay, null);

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

        public event EventHandler ItemSwipped = (e, a) => { };
        public event EventHandler ItemTapped = (e, a) => { };

        public CustomImageButton()
        {
            Initialize();
        }

        public void Initialize()
        {
            GestureRecognizers.Add(new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Up,
                Command = new Command((obj) =>
                {
                    ItemSwipped(this, EventArgs.Empty);
                })
            });
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command((obj) =>
                {
                    ItemTapped(this, EventArgs.Empty);
                })
            });

        }
    }
}

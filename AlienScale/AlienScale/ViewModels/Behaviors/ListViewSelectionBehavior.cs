using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.ViewModels.Behaviors
{
    public class ListViewSelectionBehavior : Behavior<ListView>
    {

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
                                                                    propertyName: "Command",
                                                                    returnType: typeof(ICommand),
                                                                    declaringType: typeof(EntryCompletedBehavior),
                                                                    defaultValue: null);
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

        protected override void OnAttachedTo(ListView lv)
        {
            base.OnAttachedTo(lv);
            lv.ItemSelected += OnItemSelected;
            lv.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(ListView lv)
        {
            base.OnDetachingFrom(lv);
            lv.ItemSelected -= OnItemSelected;
            lv.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            var lv = (ListView)sender;
            BindingContext = lv?.BindingContext;
        }

        void OnItemSelected(object sender, EventArgs args)
        {
            ListView lv = (ListView)sender;
            Command.Execute(null);
        }
    }
}

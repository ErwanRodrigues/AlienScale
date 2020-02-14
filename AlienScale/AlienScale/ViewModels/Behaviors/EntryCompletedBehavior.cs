using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.ViewModels.Behaviors
{
    public class EntryCompletedBehavior : Behavior<Entry>
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
                return (ICommand) GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.Completed += OnEntryCompleted;
            entry.BindingContextChanged += OnBindingContextChanged;
            entry.Unfocused += OnEntryCompleted;
        }
        
        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.Completed -= OnEntryCompleted;
            entry.BindingContextChanged -= OnBindingContextChanged;
            entry.Unfocused -= OnEntryCompleted;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            BindingContext = entry?.BindingContext;
        }

        void OnEntryCompleted(object sender, EventArgs args)
        {
            //getting cities list from GeoAPI
            Entry zpCode = (Entry)sender;
            Command.Execute(zpCode.Text);
        }
    }
}

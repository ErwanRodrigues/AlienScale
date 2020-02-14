
using AlienScale.Droid.CustomRenderers;
using AlienScale.Views.CustomControls;
using Android.Content;
using Android.Text.Method;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDecimalEntry), typeof(CustomDecimalEntryRenderer))]
namespace AlienScale.Droid.CustomRenderers
{
    public class CustomDecimalEntryRenderer : EntryRenderer
    {
        public CustomDecimalEntryRenderer(Context context) : base(context)
        {
            //constructor mandatory with Context parameter
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null) return;
            this.Control.KeyListener = DigitsKeyListener.GetInstance("1234567890,");
        }
    }
}
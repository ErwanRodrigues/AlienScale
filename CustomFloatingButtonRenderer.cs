using AlienScale.Droid.CustomRenderers;
using AlienScale.Views.CustomViews;
using Android.Content;
using Android.Runtime;
using Android.Views;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views;

[assembly: ExportRenderer(typeof(CustomFloatingButton), typeof(FloatingButtonCustomRenderer))]
namespace AlienScale.Droid.CustomRenderers
{
    public class FloatingButtonCustomRenderer : VisualElementRenderer<Xamarin.Forms.View>
    {
        private float _initialTouchX;
        private float _initialTouchY;

        public FloatingButtonCustomRenderer(Context context) : base(context)
        {

        }

        /*protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null)
            {
                LongClick -= HandleLongClick;
            }
            if (e.NewElement != null)
            {
                LongClick += HandleLongClick;
                var dragView = Element as CustomFloatingButton;
            }

        }

        private void HandleLongClick(object sender, LongClickEventArgs e)
        {
            var dragView = Element as CustomFloatingButton;
            //stop session
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as CustomFloatingButton;
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void OnVisibilityChanged(AView.View changedView, [GeneratedEnum] ViewStates visibility)
        {
            base.OnVisibilityChanged(changedView, visibility);
            if (visibility == ViewStates.Visible)
            {

            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var floatingButton = Element as CustomFloatingButton;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    //touch point
                    _initialTouchX = e.RawX;
                    _initialTouchY = e.RawY;
                    break;
                case MotionEventActions.Move:
                    break;
                case MotionEventActions.Up:
                    int offsetX = (int)e.RawX - (int)_initialTouchX;
                    int offsetY = (int)e.RawY - (int)_initialTouchY;
                    if (Math.Abs(offsetX) < 100 && Math.Abs(offsetY) > 100)
                    {
                        floatingButton.DragUp();
                    }
                    else if (Math.Abs(offsetX) < 25 && Math.Abs(offsetY) < 25)
                    {
                        floatingButton.Tapped();
                    }
                    break;
                case MotionEventActions.Cancel:
                    break;
            }
            return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            BringToFront();
            return true;
        }*/
    }
}
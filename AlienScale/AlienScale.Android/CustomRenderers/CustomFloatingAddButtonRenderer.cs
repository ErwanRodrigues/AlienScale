using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienScale.Views.CustomControls;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AView = Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using AlienScale.Droid.CustomRenderers;
using System.ComponentModel;
using AlienScale.Constants;

[assembly: ExportRenderer(typeof(CustomFloatingAddButton), typeof(CustomFloatingAddButtonRenderer))]
namespace AlienScale.Droid.CustomRenderers
{
    public class CustomFloatingAddButtonRenderer : VisualElementRenderer<Xamarin.Forms.View>
    {
        public CustomFloatingAddButtonRenderer(Context context) : base(context)
        {

        }

        float originalX;
        float originalY;
        private float _initialTouchX;
        private float _initialTouchY;
        float dX;
        float dY;
        bool firstTime = true;
        bool touchedDown = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            
            if (e.OldElement != null)
            {
                LongClick -= HandleLongClick;
            }
            if (e.NewElement != null)
            {
                LongClick += HandleLongClick;
                var dragView = Element as CustomFloatingAddButton;
                dragView.RestorePositionCommand = new Command(() =>
                {
                    //dragView.TranslateTo(Device.Info.ScaledScreenSize.Width - (Device.Info.ScaledScreenSize.Width - originalX) - originalX - dragView.Width/2, Device.Info.ScaledScreenSize.Height - (Device.Info.ScaledScreenSize.Height - originalY) - dragView.Y - dragView.Height/2, 250);
                    //a corriger
                    dragView.TranslateTo(originalX - dragView.X, originalY - dragView.Y - 100, 250);
                    //SetX(originalX);
                    //SetY(originalY);
                });
            }
        }

        private void HandleLongClick(object sender, LongClickEventArgs e)
        {
            var dragView = Element as CustomFloatingAddButton;
            if (firstTime)
            {
                originalX = GetX();
                originalY = GetY();
                firstTime = false;
            }
            dragView.DragStarted();
            touchedDown = true;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as CustomFloatingAddButton;
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
            float x = e.RawX;
            float y = e.RawY;
            var dragView = Element as CustomFloatingAddButton;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _initialTouchX = e.RawX;
                    _initialTouchY = e.RawY;
                    if (dragView.DragMode == DragMode.Touch)
                    {
                        if (!touchedDown)
                        {
                            if (firstTime)
                            {
                                originalX = _initialTouchX;
                                originalY = _initialTouchY;
                                firstTime = false;
                            }
                            dragView.DragStarted();
                        }

                        touchedDown = true;
                    }
                    dX = x - this.GetX();
                    dY = y - this.GetY();
                    break;

                case MotionEventActions.Move:
                    /*if (touchedDown)
                    {
                        if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Horizontal)
                        {
                            SetX(x - dX);
                        }

                        if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Vertical)
                        {
                            SetY(y - dY);
                        }

                    }*/
                    break;
                case MotionEventActions.Up:
                    touchedDown = false;
                    dragView.DragEnded();

                    int offsetX = (int)e.RawX - (int)_initialTouchX;
                    int offsetY = (int)e.RawY - (int)_initialTouchY;

                    //if move is lower than 10 pix, we change flyout visibility status
                    if (Math.Abs(offsetX) < 10 && Math.Abs(offsetY) < 10)
                    {
                        dragView.Tap();
                    }
                    break;
                case MotionEventActions.Cancel:
                    touchedDown = false;
                    break;
            }
            return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            BringToFront();
            return true;
        }
    }
}
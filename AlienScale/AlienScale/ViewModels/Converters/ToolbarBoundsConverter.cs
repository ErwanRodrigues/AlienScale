using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AlienScale.ViewModels.Converters
{
    class ToolbarBoundsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                Rectangle layout = (Rectangle)value;
                var screenHeight = Device.Info.PixelScreenSize.Height;
                layout.Width = Device.Info.PixelScreenSize.Width;
                // Let's apply some rules
                if (screenHeight > 2400) layout.Height = 110;
                else if (screenHeight >= 1920) layout.Height = 96;
                else if (screenHeight <= 780) layout.Height = 72;
                
                return layout;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GET Error {ex.Message}");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}

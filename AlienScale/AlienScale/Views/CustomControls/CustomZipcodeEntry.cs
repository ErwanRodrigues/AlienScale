using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlienScale.Views.CustomControls
{
    public class CustomZipCodeEntry : Entry
    {
        public void ZipCodeEntry_Completed(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text;
        }
    }
}

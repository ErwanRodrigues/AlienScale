using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlienScale.Views.CustomControls
{
    public class CustomListView : ListView
    {
        public void ListView_ItemSelected(object sender, EventArgs e)
        {
            var text = ((ListView)sender).SelectedItem;
        }
    }
}

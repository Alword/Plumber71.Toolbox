using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Plumber71.Toolbox
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var inflatedView = inflater.Inflate(Resource.Layout.HomeFragment, container, false);

            Button excelLoad = inflatedView.FindViewById<Button>(Resource.Id.excelLoad);
            excelLoad.Click += ExcelLoad_Click;
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            return inflater.Inflate(Resource.Layout.HomeFragment, container, false);
        }

        private void ExcelLoad_Click(object sender, EventArgs e)
        {
            
        }
    }
}
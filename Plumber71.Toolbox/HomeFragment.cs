using System;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Plumber71.Core.Controller;
using static Android.Views.View;

namespace Plumber71.Toolbox
{
    public class HomeFragment : Android.Support.V4.App.Fragment, IOnClickListener
    {
        private PlumberProductController plumber = null;
        private EditText globalPriceMarkupText = null;
        private Button excelLoad = null;
        private Button chacheUpdateButton = null;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var inflatedView = inflater.Inflate(Resource.Layout.HomeFragment, container, false);

            excelLoad = inflatedView.FindViewById<Button>(Resource.Id.excelLoad);

            excelLoad.Click += ExcelLoad_Click;

            globalPriceMarkupText = inflatedView.FindViewById<EditText>(Resource.Id.globalPriceMarkupText);

            globalPriceMarkupText.Text = $"{plumber.PriceMarkup.GetGlobalRate()}";

            globalPriceMarkupText.TextChanged += GlobalPriceMarkupText_TextChanged;

            chacheUpdateButton = inflatedView.FindViewById<Button>(Resource.Id.chacheUpdateButton);

            chacheUpdateButton.Click += ChacheUpdateButton_Click;

            string content = ReadWooClientAsset(inflatedView);

            plumber = new PlumberProductController(content);

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflatedView;
        }

        private async void ChacheUpdateButton_Click(object sender, EventArgs e)
        {
            await plumber.LoadOnDevice();
        }

        private void GlobalPriceMarkupText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            double rate = double.Parse(globalPriceMarkupText.Text);
            plumber.PriceMarkup.SetGlobalRate(rate);
        }

        private static string ReadWooClientAsset(View inflatedView)
        {
            string content;
            using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("woo.secret.json")))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }

        private void ExcelLoad_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionGetContent);
            intent.AddCategory(Intent.CategoryOpenable);

            intent.SetType("*/*");

            StartActivityForResult(Intent.CreateChooser(intent, "ChooseFile"), 1);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1 && data != null)
            {
                string path = FileUtil.GetActualPathFromFile(data.Data);
                plumber.UpdatePricesFromExcel(path);
            }
        }

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}
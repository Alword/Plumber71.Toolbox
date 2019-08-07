using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Plumber71.Core.Controller;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Plumber71.Toolbox
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        private PlumberProductController plumber = null;
        private EditText globalPriceMarkupText = null;
        private Button ExcelLoadButton = null;
        private Button chacheUpdateButton = null;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var inflatedView = inflater.Inflate(Resource.Layout.HomeFragment, container, false);

            string content = ReadWooClientAsset(inflatedView);

            plumber = new PlumberProductController(content);

            ExcelLoadButton = inflatedView.FindViewById<Button>(Resource.Id.excelLoad);
            globalPriceMarkupText = inflatedView.FindViewById<EditText>(Resource.Id.globalPriceMarkupText);
            chacheUpdateButton = inflatedView.FindViewById<Button>(Resource.Id.chacheUpdateButton);

            globalPriceMarkupText.Text = $"{plumber.PriceMarkup.GetGlobalRate()}";
            ExcelLoadButton.Click += ExcelLoad_Click;
            chacheUpdateButton.Click += ChacheUpdateButton_Click;
            globalPriceMarkupText.TextChanged += GlobalPriceMarkupText_TextChanged;

            plumber.ProductDownloader.OnProductDownloaded += ProductDownloader_OnProductDownloaded;
            plumber.ProductsUpdater.OnProductsUpdated += ProductsUpdater_OnProductsUpdated;
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflatedView;
        }

        private void ProductsUpdater_OnProductsUpdated(int totalUploaded)
        {
            Activity.RunOnUiThread(() =>
            {
                Toast.MakeText(Application.Context, $"Обновлени, обновлено: {totalUploaded}", ToastLength.Short).Show();
            });
        }

        private void ProductDownloader_OnProductDownloaded(int totalCount)
        {
            Activity.RunOnUiThread(() =>
            {
                Toast.MakeText(Application.Context, $"Загрузка, скачано: {totalCount}", ToastLength.Short).Show();
            });
        }

        private async void ChacheUpdateButton_Click(object sender, EventArgs e)
        {
            await plumber.LoadOnDevice();
        }

        private void GlobalPriceMarkupText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            double.TryParse(globalPriceMarkupText.Text, out double result);
            plumber.PriceMarkup.SetGlobalRate(result);
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
                Task.Run(() => plumber.UpdatePricesFromExcel(path));
            }
        }
    }
}
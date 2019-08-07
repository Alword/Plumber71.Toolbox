using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Plumber71.Toolbox
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private FrameLayout fragmentContainer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            fragmentContainer = FindViewById<FrameLayout>(Resource.Id.fragmentContainer);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);

            navigation.SetOnNavigationItemSelectedListener(this);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragmentContainer, new HomeFragment()).Commit();

            CheckAppPermissions();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Android.Support.V4.App.Fragment selectedFragment = null;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    selectedFragment = new HomeFragment();
                    break;
                case Resource.Id.navigation_dashboard:
                    selectedFragment = new TableFragment();
                    break;
                case Resource.Id.navigation_notifications:
                    return false;
            }

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragmentContainer, selectedFragment).Commit();

            return true;
        }

        private void CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                    RequestPermissions(permissions, 1);
                }
            }
        }
    }
}


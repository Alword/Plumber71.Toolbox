using Android.OS;
using Android.Views;

namespace Plumber71.Toolbox
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class TableFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            return inflater.Inflate(Resource.Layout.TableFragment, container, false);
        }
    }
}
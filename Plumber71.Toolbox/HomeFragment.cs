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
using static Android.Views.View;

namespace Plumber71.Toolbox
{
    public class HomeFragment : Android.Support.V4.App.Fragment, IOnClickListener
    {
        Button excelLoad = null;
        Android.Support.V7.App.AlertDialog dialog = null;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var inflatedView = inflater.Inflate(Resource.Layout.HomeFragment, container, false);

            excelLoad = inflatedView.FindViewById<Button>(Resource.Id.excelLoad);

            excelLoad.SetOnClickListener(this);

            dialog = new EDMTDialog.EDMTDialogBuilder().SetContext(Application.Context).Build(); //inflatedView

            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.HomeFragment, container, false);
        }

        private void ExcelLoad_Click(object sender, EventArgs e)
        {
            String[] mimeTypes =
            {"application/msword","application/vnd.openxmlformats-officedocument.wordprocessingml.document", // .doc & .docx
                    "application/vnd.ms-powerpoint","application/vnd.openxmlformats-officedocument.presentationml.presentation", // .ppt & .pptx
                    "application/vnd.ms-excel","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", // .xls & .xlsx
                    "text/plain",
                    "application/pdf",
                    "application/zip"};

            Intent intent = new Intent(Intent.ActionGetContent);
            intent.AddCategory(Intent.CategoryOpenable);


            intent.SetType(mimeTypes.Length == 1 ? mimeTypes[0] : "*/*");
            if (mimeTypes.Length > 0)
            {
                intent.PutExtra(Intent.ExtraMimeTypes, mimeTypes);
            }

            StartActivityForResult(Intent.CreateChooser(intent, "ChooseFile"), 1000);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1000)
            {
                dialog.Show();
                Console.WriteLine(data.DataString);
            }
        }

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace PS4_Tool_v2
{
    [Activity(Label = "Import", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ImportActivity : AppCompatActivity
    {

        readonly string pathToFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/PS4 Tool/";

        CheckBox firstCheckBox; //1.76 and 4.05
        CheckBox secondCheckBox; //4.55
        CheckBox thirdCheckBox; //5.05
        Button importBtn;

        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.import_layout);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.import_toolbar);
            SetSupportActionBar(toolbar);

            toolbar.Title = "Import";

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            firstCheckBox = FindViewById<CheckBox>(Resource.Id.firstCheckBox);
            secondCheckBox = FindViewById<CheckBox>(Resource.Id.secondCheckBox);
            thirdCheckBox = FindViewById<CheckBox>(Resource.Id.thirdCheckBox);

            importBtn = FindViewById<Button>(Resource.Id.importPayloadsBtn);

            importBtn.Click += ImportBtn_Click;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            Functions functions = new Functions();
            functions.importFromV1(this, firstCheckBox.Checked, secondCheckBox.Checked, thirdCheckBox.Checked);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
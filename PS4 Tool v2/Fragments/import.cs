using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace PS4_Tool_v2.Fragments
{
    public class Import : Fragment
    {
        readonly string pathToFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/PS4 Tool/";

        private CheckBox firstCheckBox; //1.76 and 4.05
        private CheckBox secondCheckBox; //4.55
        private CheckBox thirdCheckBox; //5.05
        private Button importBtn;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            firstCheckBox = View.FindViewById<CheckBox>(Resource.Id.firstCheckBox);
            secondCheckBox = View.FindViewById<CheckBox>(Resource.Id.secondCheckBox);
            thirdCheckBox = View.FindViewById<CheckBox>(Resource.Id.thirdCheckBox);

            importBtn = View.FindViewById<Button>(Resource.Id.importPayloadsBtn);

            importBtn.Click += ImportBtn_Click;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            Functions functions = new Functions();
            functions.importFromV1(Context.ApplicationContext, firstCheckBox.Checked, secondCheckBox.Checked, thirdCheckBox.Checked);
        }


        public static Import NewInstance()
        {
            var frag1 = new Import { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.import_fragment, container, false);
        }
    }
}
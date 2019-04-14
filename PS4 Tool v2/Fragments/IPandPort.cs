using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace PS4_Tool_v2.Fragments
{
    public class IPandPort : Fragment
    {
        private Button saveBtn;
        private EditText ipTxt;
        private EditText portTxt;

        readonly string pathToFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/PS4 Tool/";


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            saveBtn = View.FindViewById<Button>(Resource.Id.saveButton);
            ipTxt = View.FindViewById<EditText>(Resource.Id.PS4IPAdressTextBox);
            portTxt = View.FindViewById<EditText>(Resource.Id.portTextBox);

            SaveSettings settings = new SaveSettings();
            settings.Load();
            portTxt.Text = settings.port;
            ipTxt.Text = settings.IPAdress;

            saveBtn.Click += SaveBtn_Click;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveSettings settings = new SaveSettings();
            settings.IPAdress = ipTxt.Text;
            settings.port = portTxt.Text;
            settings.Save();
            Toast.MakeText(Context.ApplicationContext, "Saved!", ToastLength.Short).Show();
        }

        public static IPandPort NewInstance()
        {
            var frag1 = new IPandPort { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.ipandport_fragment, container, false);
        }
    }
}
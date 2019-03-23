using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;
using System.IO;

namespace PS4_Tool_v2.Fragments
{
    public class Settings : Fragment
    {
        private Button saveBtn;
        private Button importBtn;
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
            importBtn = View.FindViewById<Button>(Resource.Id.importBtn);
            ipTxt = View.FindViewById<EditText>(Resource.Id.PS4IPAdressTextBox);
            portTxt = View.FindViewById<EditText>(Resource.Id.portTextBox);

            SaveSettings settings = new SaveSettings();
            settings.Load();
            portTxt.Text = settings.port;
            ipTxt.Text = settings.IPAdress;

            saveBtn.Click += SaveBtn_Click;
            importBtn.Click += ImportBtn_Click;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(pathToFolder + "1.764.05") || Directory.Exists(pathToFolder + "4.55") || Directory.Exists(pathToFolder + "5.05"))
            {
                Intent intent = new Intent(Context.ApplicationContext, typeof(PS4_Tool_v2.ImportActivity));
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(Context.ApplicationContext, "You don't have any Payload to Import", ToastLength.Short).Show();
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SaveSettings settings = new SaveSettings();
            settings.IPAdress = ipTxt.Text;
            settings.port = portTxt.Text;
            settings.Save();
            Toast.MakeText(Context.ApplicationContext, "Saved!", ToastLength.Short).Show();
        }

        public static Settings NewInstance()
        {
            var frag1 = new Settings { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.settings_fragment, container, false);
        }
    }
}
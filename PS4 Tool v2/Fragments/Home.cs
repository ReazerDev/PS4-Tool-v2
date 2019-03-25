using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using System;
using System.Collections.Generic;

namespace PS4_Tool_v2.Fragments
{
    public class Home : Fragment
    {

        string IPAdress;
        string Port;

        MaterialSpinner payloadSpinner;
        FloatingActionButton refreshBtn;
        Button sendBtn;
        
        private static bool mPermissionGranted;

        private List<string> payloadList;

        private ArrayAdapter<string> adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

            payloadSpinner = View.FindViewById<MaterialSpinner>(Resource.Id.payloadSpinner);
            refreshBtn = View.FindViewById<FloatingActionButton>(Resource.Id.refreshBtn);
            sendBtn = View.FindViewById<Button>(Resource.Id.sendBtn);

            loadFiles();

            SaveSettings settings = new SaveSettings();
            settings.Load();
            Port = settings.port;
            IPAdress = settings.IPAdress;

            sendBtn.Click += SendBtn_Click;
            refreshBtn.Click += RefreshBtnOnClick;
            loadFiles();
        }

        private void loadFiles()
        {
            if (mPermissionGranted)
            {
                Functions functions = new Functions();
                functions.createDirectories(Context.ApplicationContext);
                functions.getFilesInDownload();
                functions.moveFromAssets(Context.ApplicationContext);

                payloadList = functions.getFilesInDirectory();

                adapter = new ArrayAdapter<string>(Context.ApplicationContext, Resource.Layout.payload_spinner_item, payloadList);
                payloadSpinner.Adapter = adapter;
            }
        }

        private void RefreshBtnOnClick(object sender, EventArgs e)
        {
            Toast.MakeText(Context.ApplicationContext, "Refreshing...", ToastLength.Short).Show();
            loadFiles();
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            if (payloadSpinner.SelectedItemPosition == 0)
            {
                Toast.MakeText(Context.ApplicationContext, "Please select a Paylaod!", ToastLength.Short).Show();
                return;
            }
            Functions functions = new Functions();
            string file = payloadList[payloadSpinner.SelectedItemPosition - 1];
            functions.Send(Context.ApplicationContext, file);
                
        }

        public static Home NewInstance(bool permissionGranted)
        {
            var frag1 = new Home { Arguments = new Bundle() };
            mPermissionGranted = permissionGranted;
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.main_fragment, container, false);
        }


    }
}
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using PS4_Tool_v2.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;
using System;
using Android.Runtime;

namespace PS4_Tool_v2
{
    [Activity(Label = "@string/app_name", MainLauncher = true,  LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : AppCompatActivity
    {

        string Port;
        string IPAdress;

        bool permissionGranted = false;
        const int permission_Request = 101;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        IMenuItem previousItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestPermissions();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);

            SaveSettings settings = new SaveSettings();
            settings.Load();
            Port = settings.port;
            IPAdress = settings.IPAdress;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            
            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                if (previousItem != null)
                    previousItem.SetChecked(false);

                navigationView.SetCheckedItem(e.MenuItem.ItemId);

                previousItem = e.MenuItem;

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_fan_control:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_ipandport:
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_import:
                        ListItemClicked(3);
                        break;
                    case Resource.Id.nav_credits:
                        ListItemClicked(4);
                        break;
                }


                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                navigationView.SetCheckedItem(Resource.Id.nav_home);
                ListItemClicked(0);
            }
            
        }

        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = Home.NewInstance(permissionGranted);
                    SupportActionBar.Title = "Payload Sender";
                    break;
                case 1:
                    fragment = FanControl.NewInstance();
                    SupportActionBar.Title = "Fan Control";
                    break;
                case 2:
                    fragment = IPandPort.NewInstance();
                    SupportActionBar.Title = "IP and Port";
                    break;
                case 3:
                    fragment = Import.NewInstance();
                    SupportActionBar.Title = "Import Payloads from v1";
                    break;
                case 4:
                    fragment = Credits.NewInstance();
                    SupportActionBar.Title = "Credits";
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        #region RuntimePermissions

        public void RequestPermissions()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted && ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, permission_Request);
                }
                else
                {
                    permissionGranted = true;
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case permission_Request:
                    {
                        // If request is cancelled, the result arrays are empty.
                        if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                        {
                            permissionGranted = true;
                            ListItemClicked(1);
                            ListItemClicked(0);
                        }
                        else
                        {
                            var msgBox = (new Android.App.AlertDialog.Builder(this)).Create();
                            msgBox.SetTitle("Warning");
                            msgBox.SetMessage("You need to accept the Permission, in order to use the App!");
                            msgBox.SetButton("Ok", delegate
                            {
                                RequestPermissions();
                            });
                            msgBox.SetButton2("Cancel", delegate
                            {
                                System.Environment.Exit(0);
                            });
                            msgBox.Show();
                        }
                        return;
                    }

                    // other 'case' lines to check for other
                    // permissions this app might request
            }
        }

        #endregion
    }
}


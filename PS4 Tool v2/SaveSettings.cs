using System;
using Android.Content;
using Android.App;

namespace PS4_Tool_v2
{
    [Serializable]
    public class SaveSettings
    {
        public string IPAdress { get; set; }
        public string port { get; set; }

        public void Save()
        {
            var prefs = Application.Context.GetSharedPreferences("PS4 Tool", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("IP Adress", IPAdress);
            prefEditor.PutString("Port", port);
            prefEditor.Commit();
        }

        public void Load()
        {
            var prefs = Application.Context.GetSharedPreferences("PS4 Tool", FileCreationMode.Private);
            var ipPref = prefs.GetString("IP Adress", null);
            var portPref = prefs.GetString("Port", "9020");

            IPAdress = ipPref;
            port = portPref;
        }
    }
}